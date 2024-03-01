using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lift : MonoBehaviour
{
    [HideInInspector] public bool tavan, checkGround;
    public GameObject ok;
    public GameObject player;
    public int velocity = 5, player_velocity;
    bool reverso;
    public AudioSource sound_inceput, sound_final;
    private bool activate_sunet;
    void Start()
    {
        activate_sunet = false;
    }
    void Update()
    {
        if (tavan == true)
        {
            if (ok.GetComponent<Maneta>().active_maneta == 0)
                velocity = 5;
        }
        else
        if (checkGround == true)
        {
            if (ok.GetComponent<Maneta>().active_maneta == 1)
                velocity = 5;
        }
        if (ok.GetComponent<Maneta>().active_maneta == 1)
        {
            if (tavan == false)
            {
                if (activate_sunet == false)
                {
                    activate_sunet = true;
                    sound_inceput.Play();
                }

                reverso = true;
                checkGround = false;
                transform.position = new Vector2(transform.position.x, transform.position.y + velocity * Time.deltaTime);
                player.transform.position = new Vector2(player.transform.position.x, player.transform.position.y + player_velocity * Time.deltaTime);
            }
        }
        else
        {
            if (ok.GetComponent<Maneta>().active_maneta == 0 && reverso == true)
                if (checkGround == false)
                {
                    if (activate_sunet == false)
                    {
                        activate_sunet = true;
                        sound_inceput.Play();
                    }
                    tavan = false;
                    transform.position = new Vector2(transform.position.x, transform.position.y - velocity * Time.deltaTime);
                    player.transform.position = new Vector2(player.transform.position.x, player.transform.position.y - player_velocity * Time.deltaTime);
                }
        }
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            player_velocity = 5;
        }
        if (coll.gameObject.CompareTag("Ground"))
        {
            activate_sunet = false;
            velocity = 0;
            checkGround = true;
            sound_inceput.Stop();
            sound_final.Play();
        }
    }
    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            player_velocity = 0;
        }
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Tavan"))
        {
            activate_sunet = false;
            sound_inceput.Stop();
            sound_final.Play();
            velocity = 0;
            tavan = true;
        }
    }
}
