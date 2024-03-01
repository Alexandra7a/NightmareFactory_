using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnderHand : MonoBehaviour
{
    public GameObject starter;
    public GameObject player;
    public GameObject hand;
    Vector3 temp;
    bool flip = false;
    Vector3 initial_position;
    void Awake()
    {

    }
    void Start()
    {
        initial_position=hand.transform.position;
    }
    void OnTriggerEnter2D(Collider2D trigg)
    {
        if (trigg.gameObject.CompareTag("Player"))
        {
            starter.GetComponent<Hand>().velocity = 0;
            if (flip == false)
            {
                flip = true;
                temp = hand.transform.localScale;
                temp.y *= -1f;
                hand.transform.localScale = temp;
                temp = hand.transform.position;
                temp.y += -3;
                hand.transform.position = temp;
            }
        }
    }
}
