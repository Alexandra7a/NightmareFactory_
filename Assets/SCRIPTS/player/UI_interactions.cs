using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_interactions : MonoBehaviour
{
    [SerializeField] private Image c,box,key;
        void Start()
    {
        c.enabled=false;
        box.enabled=false;
        key.enabled=false;
    }
    void Update()
    {
          if(PlayerPrefs.GetInt("KEY")==1)
        {
            key.enabled=true;
        }
    }

    private void OnTriggerEnter2D(Collider2D trigg)
    {
        if (trigg.gameObject.CompareTag("Cusca"))
        {
            c.enabled=true;
        }
        if(trigg.gameObject.CompareTag("Chest"))
        {
            box.enabled=true;
        }
       
    }
     private void OnTriggerExit2D(Collider2D trigg)
    {
        if (trigg.gameObject.CompareTag("Cusca"))
        {
            c.enabled=false;
        }
        if(trigg.gameObject.CompareTag("Chest"))
        {
            box.enabled=false;
        }
    }
    
}
