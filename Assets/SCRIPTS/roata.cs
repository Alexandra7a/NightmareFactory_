using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roata : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.CompareTag("Destroy_player"))
        {
            Destroy(gameObject);
        }
    }
}
