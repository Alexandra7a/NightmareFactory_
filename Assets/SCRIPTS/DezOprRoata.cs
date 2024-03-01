using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DezOprRoata : MonoBehaviour
{
    public GameObject opritor;
    public GameObject player;
    private Renderer r;
    private BoxCollider2D bc;
    void Awake()
    {
        r = opritor.GetComponent<Renderer>();
        bc = opritor.GetComponent<BoxCollider2D>();
    }
    void Start()
    {
        r.enabled = true;
        bc.enabled=true;
    }
    void OnTriggerEnter2D(Collider2D trigg)
    {
        if (trigg.gameObject.CompareTag("Player"))
        {
            r.enabled = false;
            bc.enabled=false;
        }
    }
}
