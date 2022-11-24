using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleIA : MonoBehaviour
{
    public float speed    = 2.5f;
    public float range=0f;
    public float rangeMax = 5f,rangeMin = -5f;

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        Vector3 direction = new Vector3(5f,0f,0f);
        direction.Normalize();
        transform.Translate(direction * speed * Time.deltaTime,Space.World);

        range += speed * Time.deltaTime;

        if(range > rangeMax || range < rangeMin) speed = -speed;
    }
}
