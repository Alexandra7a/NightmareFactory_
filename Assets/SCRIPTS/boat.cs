using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boat : MonoBehaviour
{
    private player_interactions ok;
    void Awake()
    {
          ok = GameObject.Find("PLAYER").GetComponent<player_interactions>();
    }
    void FixedUpdate()
    {
        if(GameObject.Find("PLAYER")!=null)//verificam daca playerul nu a fost distrus
        if(ok.velocity!=0)
        {
           transform.position = new Vector2(transform.position.x + ok.velocity * Time.deltaTime, transform.position.y);
           ok. transform.position=new Vector2(ok.transform.position.x+ok.velocity*Time.deltaTime, ok.transform.position.y);
            ok.boat=false;
        }
    }
    private void OnCollisionEnter2D(Collision2D coll)// tot ceea ce este legat de collisions
    {
       
    if(coll.gameObject.CompareTag("Ground"))
    {
        ok.velocity=0;
    }
    }
}
