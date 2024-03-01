using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionOilDrop : MonoBehaviour
{
    [HideInInspector] public int atins = 0;
    void OnTriggerEnter2D(Collider2D trigg)
    {
        if (trigg.gameObject.CompareTag("Player"))
             atins = 1;
             StartCoroutine(Timp());
    }
    IEnumerator Timp()// pentru a preveni sa apara pre multe picaturi
    {
        yield return new WaitForSeconds(0.001f);
        atins=0;
    }
}

