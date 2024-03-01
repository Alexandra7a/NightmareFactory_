using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilDroper : MonoBehaviour
{
    public GameObject oil;
    public GameObject coll_drop;
    public GameObject obstacol_trambulina;
    int k;//contorizam nr de picaturi
    void Start()
    {

    }
    void Update()
    {
        if(coll_drop.GetComponent<collisionOilDrop>().atins==1)
        {
            k++;
           Instantiate(oil,new Vector3(transform.position.x,transform.position.y,0f),transform.rotation);
        }
        if(k==5)
        {
            Destroy(obstacol_trambulina,2f);  
        }
    }
}
