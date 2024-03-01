using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public GameObject hand;
    private Renderer r;
    private PolygonCollider2D pc;
    [HideInInspector] public int velocity = 11, active_hand;
    void Awake()
    {
        r = hand.GetComponent<Renderer>();
        pc = hand.GetComponent<PolygonCollider2D>();
    }
    void Start()
    {
        r.enabled = false;
        pc.enabled = false;
    }
    void FixedUpdate()
    {
        if (active_hand == 1)
        {
            hand.transform.position = new Vector2(hand.transform.position.x + velocity * Time.deltaTime, hand.transform.position.y);
        }
    }
    void OnTriggerStay2D(Collider2D trigg)
    {
        if (trigg.gameObject.CompareTag("Player"))
        {
            r.enabled = true;
            pc.enabled = true;
            active_hand = 1;
        }
    }
}
