using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maneta : MonoBehaviour
{
    //compnente
    private Animator anim;
    //animatii
    //variabile
    [HideInInspector] public int active_maneta;
  private  AudioSource sound;
    void Awake()
    {
        anim = GetComponent<Animator>();
        sound=GetComponent<AudioSource>();
    }
    void Start()
    {
        active_maneta = 0;
    }
    private void OnTriggerEnter2D(Collider2D trigg)
    {

        if (trigg.gameObject.CompareTag("Player"))
        {
            if (active_maneta == 0)
            {
                 anim.Play("right");
                 sound.Play();
                 active_maneta =1;
            }
         else
            if (active_maneta == 1)
            {
                anim.Play("left");
                sound.Play();
                active_maneta = 0;
            }
                
        }
    }
}
