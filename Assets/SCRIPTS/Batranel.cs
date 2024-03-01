using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Batranel : MonoBehaviour
{
    private int directie;
     private Vector3 temp;
     public Maneta Maneta_distrugere;
    void Start()
    {
        directie=-1;
    }
    void FixedUpdate()
    {
       transform.position += new Vector3(directie, 0f, 0f) * Time.deltaTime * 5;
    }
    private void OnTriggerEnter2D(Collider2D trigg)
    {
        if(trigg.gameObject.CompareTag("Zid"))
        {
               directie*=-1;
               Flip();
        }
    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.CompareTag("Batranel"))
        {

            if(Maneta_distrugere.active_maneta==1)
            {
                 Destroy(coll.gameObject);
            }
           
        }
        if(coll.gameObject.CompareTag("Destroy_player"))
        {
                 Destroy(gameObject);
        }
    }
    void Flip()
    {
            temp = transform.localScale;
            temp.x *= -1f;
            transform.localScale = temp;
    }
}
