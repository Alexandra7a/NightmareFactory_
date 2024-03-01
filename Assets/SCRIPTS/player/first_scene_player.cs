using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class first_scene_player : MonoBehaviour
{
    // vizibile
    [SerializeField] private float speed;
    [SerializeField] private float JumpForce;
    //cmponente
    public GameObject mainCamera;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    private Vector3 temp;
    public Transform cp;// CheckPint0
    public Settings IsTheGamePased;
    SavePositionManager save;
    public AudioSource sound, sound2;
    //taguri si animatii
    private string GROUND_TAG = "Ground";
    private string WALK_ANIMATION = "walk";
    private string DESTROY_PLAYER_TAG = "Destroy_player";
    private string DESTROY_ANIMATION = "destroy";
    //variabile 
    private float movementX, movementY;
    private bool flip, isGrounded = true, doubleJump;
    Vector3 pos;
    public static int firstOpening = 0;

    static Vector3 scene_pos;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        cp = GameObject.Find("CheckPoint0").GetComponent<Transform>();// gasesti obiectul din scena de care detine scriptul pe care vrei sa il accsezi
        save = GetComponent<SavePositionManager>();
        PlayerPrefs.SetInt("INDEX", save.IndexScene());
    }
    void Start()
    {
        firstOpening = 3;
        // ne trebuie o variabila care sa spuna daca e apasat butonul new sau continue 
        if (SavePositionManager.NewButtonPressed == 1)
            save.StartPosition();
        else
        {
            mainCamera.GetComponent<AudioListener>().enabled = true; //activam audioul pentru scena daca vine de la butonul de continua
            save.Load();
        }

        flip = true;
    }
    void Update()
    {
        if (IsTheGamePased.paused == false)
        {
            movementX = Input.GetAxis("Horizontal");
            if (movementX != 0 && isGrounded == true)
            {
                if (!sound.isPlaying)
                    sound.Play();

            }
            else
                sound.Stop();
            Animations();
            Jump();
        }
    }
    void FixedUpdate()
    {
        scene_pos = transform.position += new Vector3(movementX, 0f, 0f) * Time.deltaTime * speed;
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
    void Flip(float movementX)// nu a mers flipu de la sprite renderer asa ca am accesat scale din transform 
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
            if (isGrounded)
            {
                sound2.Play();
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
    private void OnTriggerEnter2D(Collider2D trigg)
    {
        if (trigg.gameObject.CompareTag("portal_to_scene1"))
        {
            save.Portal();
            SceneManager.LoadScene(2);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)// tot ceea ce este legat de collisions
    {
        if (collision.gameObject.CompareTag(GROUND_TAG))
            isGrounded = true;
        if (collision.gameObject.CompareTag(DESTROY_PLAYER_TAG))
        {
            anim.SetBool(DESTROY_ANIMATION, true);
            StartCoroutine(Respawn());
        }
    }
    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(1.5f);
        anim.SetBool(DESTROY_ANIMATION, false);
        pos.x = cp.position.x;
        pos.y = cp.position.y;
        transform.position = pos;
    }
}
