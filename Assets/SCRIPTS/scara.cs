using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scara : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform tr;
    [HideInInspector] public int active_scara;
    public float speed = 5;
    private float movementX, movementY;
    void Awake()
    {
        rb = GameObject.Find("PLAYER").GetComponent<Rigidbody2D>();
        tr = GameObject.Find("PLAYER").GetComponent<Transform>();
    }
    private void OnTriggerEnter2D(Collider2D trigg)
    {
        if (trigg.gameObject.CompareTag("Player"))
        {
            active_scara = 1;
            rb.gravityScale = 0;
        }
    }
    void OnTriggerExit2D(Collider2D trigg)
    {
        if (trigg.gameObject.CompareTag("Player"))
        {
            active_scara = 0;
            rb.gravityScale = 3;
        }      
    }
    void Update()
    {
        movementX = Input.GetAxis("Horizontal");
        movementY = Input.GetAxisRaw("Vertical");
         
    }
    void FixedUpdate()
    {
       if (active_scara == 1)
            transform.position += new Vector3(0f,movementY, 0f) * speed * Time.deltaTime;
    }

}
