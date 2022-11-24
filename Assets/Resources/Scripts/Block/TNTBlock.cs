using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNTBlock : MonoBehaviour
{
    public Material[] tnt_countdown;
    bool triggered = false;

    void Awake(){StartCoroutine(Blink());}

    public void DestroyBlock(GameObject crash)
    {
        triggered = true;
        StartCoroutine(timetoDestroy(crash));
    }

    IEnumerator Blink()
    {
        GetComponent<Renderer>().material.color = Color.white;
        yield return new WaitForSeconds(1f);
        GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(Blink());
    }

    IEnumerator timetoDestroy(GameObject crash)
    {
        if(triggered)
        {
            GetComponent<MeshRenderer> ().material = tnt_countdown[0];
            yield return new WaitForSeconds(1f);
            GetComponent<MeshRenderer> ().material = tnt_countdown[1];
            yield return new WaitForSeconds(1f);
            GetComponent<MeshRenderer> ().material = tnt_countdown[2];
            yield return new WaitForSeconds(1f);
            crash.GetComponent<CharacterScript>().boxsmash++;
            this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            GetComponent<MeshRenderer>().enabled = false;
            yield return new WaitForSeconds(2f);
            this.gameObject.SetActive(false);
        }
    }

    IEnumerator ImediatlyDestroy(GameObject crash)
    {
        crash.GetComponent<CharacterScript>().boxsmash++;
        this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(2f);
        this.gameObject.SetActive(false);
    }

    

    void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player") && !triggered)
        {
            if(!collision.gameObject.GetComponent<CharacterScript>().isattack) DestroyBlock(collision.gameObject);
            else StartCoroutine(ImediatlyDestroy(collision.gameObject));
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player") && !triggered)
        {
            if(!collision.gameObject.GetComponent<CharacterScript>().isattack) DestroyBlock(collision.gameObject);
            else StartCoroutine(ImediatlyDestroy(collision.gameObject));
        }
        
    }
}
