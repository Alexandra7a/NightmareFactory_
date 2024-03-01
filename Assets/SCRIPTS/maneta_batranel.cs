using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class maneta_batranel : MonoBehaviour
{
     public Maneta Maneta_distrugere;
    void Update()
    {
        if(Maneta_distrugere.active_maneta==1)
        Destroy(gameObject);
    }
}
