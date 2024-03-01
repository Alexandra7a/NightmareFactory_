using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDrop : MonoBehaviour
{
    private Renderer rd;
     private CapsuleCollider2D cc;
    float life=10;
    void Start()
    {
        rd=GetComponent<Renderer>();
        cc=GetComponent<CapsuleCollider2D>();
    }
    void Update()
    {
        if(life>0)
        {
            life-=Time.deltaTime;
            if(life<=0)
            {
                 rd.enabled=false;
                 cc.enabled=false;
            }
           
        }
    }
}
