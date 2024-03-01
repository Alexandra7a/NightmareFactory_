using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apa : MonoBehaviour
{
    private AudioSource sound;
    void Awake()
    {
        sound = GetComponent<AudioSource>();
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
            sound.Play();
    }
}
