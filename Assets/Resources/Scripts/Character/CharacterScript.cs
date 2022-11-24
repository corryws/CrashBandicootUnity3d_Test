using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    [Header("CHARACTER STATUS")]
    public int   life          = 3;
    public int   wumpascore    = 0;
    public int   boxsmash      = 0;

    [Header("CHARACTERT SETTING")]
    public float speed         = 0.5f;
    public float rotationSpeed = 0.5f;
    public float jumpforce     = 0.5f;

    Animator anim;
    CapsuleCollider coll;
    Rigidbody rb;

    float horizontalInput;
    float verticalInput;
    bool vulnerability = false;
    public bool  isjump   = false; 
    public bool  isdie    = false;
    public bool  isattack = false;
    


    void Awake()
    {
        anim = GetComponent<Animator>       ();
        coll = GetComponent<CapsuleCollider>();
        rb   = GetComponent<Rigidbody>      ();
    }
    
    void FixedUpdate()
    {
        if(life <= 0)
        {
            life = 0;
            anim.SetBool("isDie",true);
        }else
        {
            CharacterMovement();
            if(!isjump || !isattack) SetCollider(0f,0.75f,0f,0.5f,1.5f);
        }
        
    }

    void CharacterMovement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput   = Input.GetAxis("Vertical");

        if(horizontalInput != 0 || verticalInput != 0) anim.SetBool("isRun", true); else anim.SetBool("isRun", false);

        Vector3 direction = new Vector3(horizontalInput,0f,verticalInput);
        direction.Normalize(); transform.Translate(direction * speed * Time.deltaTime,Space.World);

        if(direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation (direction,Vector3.up);
            transform.rotation    = Quaternion.RotateTowards(transform.rotation,toRotation,rotationSpeed);
        }

        Getattack();
        Jump();
    }

    void Getattack()
    {
        if(isattack) SetCollider(0f,0.75f,0f,1f,2f);
        else if(Input.GetKey("e") && !isattack) anim.SetBool("isAttack",true);
    }

    void Jump()
    {
        if(Input.GetKey(KeyCode.Space) && !isjump)
        {
            SetCollider(02f,1.2f,0f,0.5f,1.15f);
            rb.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
            anim.SetBool("isJump",true);
            isjump = true;
        }
    }

    void SetCollider(float x,float y,float z,float r ,float h)
    {
        coll.center = new Vector3(x,y,z);
        coll.radius = r;
        coll.height = h;
    }

    void TakeDamage(int damage)
    {
        vulnerability = true;
        if(vulnerability) 
        {
            life-=damage;
            StartCoroutine(SetInvincible(true));
        }
    }

    IEnumerator SetInvincible(bool isvulnerabile)
    {
        transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = Color.grey;
        yield return new WaitForSeconds(1f);
        transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = Color.white;
        yield return new WaitForSeconds(1f);
        vulnerability = false;
    }

//COLLISION FUNCTION
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Terrain")  ||
           collision.gameObject.CompareTag("normalBox")||
           collision.gameObject.CompareTag("tntBox"))
        {
            isjump = false;
            anim.SetBool("isJump",false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Wumpa"))
        {
            wumpascore+=1;
            other.gameObject.SetActive(false);
        }

        if(other.gameObject.CompareTag("esplosion")) TakeDamage(1);
        
    }
}
