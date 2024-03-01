using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacol1 : MonoBehaviour
{
    public GameObject g;
    private Maneta g_maneta;
    // Start is called before the first frame update
    void Start()
    {
        g_maneta = g.GetComponent<Maneta>();
    }
    void Update()
    {
         if (g_maneta.active_maneta == 1)
         { 
             Destroy(gameObject);
         }
         
           
    }
}
