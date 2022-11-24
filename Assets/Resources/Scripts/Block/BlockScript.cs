using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour
{
    public GameObject wumpaprefab;

    public void DestroyBlock()
    {
        StartCoroutine(timeAttack());
    }

    IEnumerator timeAttack()
    {
        GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        Instantiate(wumpaprefab,this.transform.position,Quaternion.identity);
        this.gameObject.SetActive(false);
    }

    void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<CharacterScript>().isattack)
        {
            DestroyBlock();
            collision.gameObject.GetComponent<CharacterScript>().boxsmash++;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<CharacterScript>().isjump)
        {
            DestroyBlock();
            collision.gameObject.GetComponent<CharacterScript>().boxsmash++;
        }
    }
}
