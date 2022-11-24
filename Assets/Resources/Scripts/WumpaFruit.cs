using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WumpaFruit : MonoBehaviour
{
    public float speed = 50f;

    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0f,speed*Time.deltaTime,0f));
    }
}
