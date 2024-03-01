using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
//using UnityEngine.AudioClip;
public class player_movement : MonoBehaviour
{
    // vizibile
    [SerializeField] private float speed;
    [SerializeField] private float JumpForce;
    //cmponente
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    private Vector3 temp;
    private player_interactions ok;
   // private AudioSource ;
    public  AudioSource sound,sound2;
    public Settings IsTheGamePased;
    //taguri si animatii
    private string GROUND_TAG = "Ground";
    private string TOMOVE_TAG = "ToMove";
    private string WALK_ANIMATION = "walk";
    //variabile 
    private float movementX, movementY;
    private bool flip, isGrounded = true, doubleJump;
    public static int  firstOpening=0;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        ok = GetComponent<player_interactions>();
    }
    void Start()
    {
        firstOpening=3;
        flip = true;
    }
    void Update()
    {
        if (IsTheGamePased.paused == false)
        {
            movementX = Input.GetAxis("Horizontal");
            if (movementX != 0&&isGrounded==true)
            {
                if(!sound.isPlaying)
                sound.Play();
               
            } else
                sound.Stop();
                
            Animations();
            if (ok.active_scara == 0)//daca nu urca pe scara poate sari 
                Jump();
        }

    }
    void FixedUpdate()
    {
        transform.position += new Vector3(movementX, 0f, 0f) * Time.deltaTime * speed;
    }
    void Animations()
    {
        if (movementX > 0)
        {
            Flip(movementX);
            anim.SetBool(WALK_ANIMATION, true);
        }
        else if (movementX < 0)
        {
            Flip(movementX);
            anim.SetBool(WALK_ANIMATION, true);
        }
        else
        {
            anim.SetBool(WALK_ANIMATION, false);
        }
    }
    void Flip(float movementX)
    {
        if (movementX > 0 && !flip || movementX < 0 && flip)
        {
            flip = !flip;
            temp = transform.localScale;
            temp.x *= -1f;
            transform.localScale = temp;
        }
    }
    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        { 
            sound2.Play();
            if (isGrounded)
            {
               
                doubleJump = true;
                isGrounded = false;// pentru a putea sari doar o singura data 
                anim.SetBool(WALK_ANIMATION, false);// pentru a putea sari in mai multe parti(animatia de walk interactiona cu butoanele si nu permitea jump-ul cand weau apasate aoi tastele w, d ori a, w)
                rb.velocity = Vector2.up * JumpForce;
            }
            else
            {
                if (doubleJump)
                {
                    doubleJump = false;
                    rb.velocity = Vector2.up * (JumpForce - 3);
                }
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GROUND_TAG) || collision.gameObject.CompareTag(TOMOVE_TAG))// pentru a putea sari si pe obiecte 
            isGrounded = true;
    }
}
