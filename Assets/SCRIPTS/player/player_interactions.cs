using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player_interactions : MonoBehaviour
{
    public float DragForce, ScaraForce;
    //componente
    public GameObject FinalTransition;
    private Animator anim;
    private Collider2D cd;
    private player_movement pm;
    [HideInInspector] public Rigidbody2D rb;
    public SavePositionManager save;
    public SaveSystem system;// pentru a prelua datele legat de pozite si checkpoint-uri
    public Parent parent;
    public AudioSource trampoline_sound, collect_sound;
    //taguri si animatii
    private string GROUND_TAG = "Ground";
    private string DESTROY_TAG = "Destroy";
    private string DESTROY_ANIMATION = "destroy";
    private string DESTROY_PLAYER_TAG = "Destroy_player";
    private string DESTROY_PLAYER_INSTANTLY_TAG = "Destroy_player_instantly";
    //variabile
    [HideInInspector] public float movementX, movementY;
    public bool isGrounded, can_press;
    Vector3 pos;
    [HideInInspector] public int active_drag, active_scara;
    [HideInInspector] public int trigg = 0;
    [HideInInspector] public float velocity;
    public static Vector3 player_pos;
    public float upSpeed;
    [HideInInspector] public bool boat;// pentru ca atunci cand atingi baraca a doua oara sa nu se mai deplaseze 
    public static int checkpoint = 0, chest = 0;
    void Awake()
    {
        anim = GetComponent<Animator>();
        pm = GetComponent<player_movement>();
        rb = GetComponent<Rigidbody2D>();
        save = GetComponent<SavePositionManager>();
    }
    void Start()
    {
        PlayerPrefs.SetInt("KEY", 0);//in prima faza, cheia nu e luata
        system.Load();
        int i = save.IndexScene();
        if (SavePositionManager.PortalLevel1 == 1)//ori vine de la portal
        {
            pos = transform.position;
            pos.x = 127.68f;
            pos.y = -1260.57f;
            transform.position = pos;
            SavePositionManager.PortalLevel1 = 0;// il facem 0 ca sa mearga butonul de continua 
        }
        else
        {
            if (checkpoint == 1)// ori vine de la checkpoint
            {
                player_pos = transform.position;
                player_pos.x = system.activeSave.lastCP.x;//accesam fisierul xml pentru a furniza pozitiile 
                player_pos.y = system.activeSave.lastCP.y;
                transform.position = player_pos;
                checkpoint = 0;// il facem din nou zero in cazul in care avem apasat butonul continue. Daca nu am avea linia asta atunci la apsarea butonului continua s-ar executa checkpointul nu load function 
            }
            else
            {
                player_pos = transform.position;
                player_pos.x = system.activeSave.pos.x;
                player_pos.y = system.activeSave.pos.y;
                transform.position = player_pos;
            }

        }
        active_scara = 0;
        velocity = 0;
        boat = true;
    }
    void Update()
    {
        movementX = Input.GetAxis("Horizontal");
        movementY = Input.GetAxisRaw("Vertical");
        if (isGrounded == true)
            upSpeed = 5;
        PlayerPrefs.SetFloat("current_x", transform.position.x);// folosit in saveSystem
        PlayerPrefs.SetFloat("current_y", transform.position.y);
        if (Input.GetKeyDown(KeyCode.O) && chest == 1 && PlayerPrefs.GetInt("KEY") == 1)// finalul jocului 
        {
            FinalTransition.GetComponent<Animator>().SetTrigger("gMorning_in");//pornim animatia de intrare
            StartCoroutine(GoodMorning());
            //aici o sa punem scena de final cu cantec de pasarele si good morning 
        }

    }
    IEnumerator GoodMorning()
    {
        FinalTransition.GetComponent<AudioSource>().Play();//pornimsunetul
        yield return new WaitForSeconds(12);
       var async = SceneManager.LoadSceneAsync(3, LoadSceneMode.Additive);
        if (!async.isDone)
        {
            yield return null;
        }
    }
    void FixedUpdate()
    {
        if (active_scara == 1)
        {
            //transform.position += new Vector3(0f, movementY, 0f) * Time.deltaTime * ScaraForce;
            rb.velocity = new Vector3(0f, movementY, 0f) * Time.deltaTime * ScaraForce;
        }
    }
    private void OnTriggerEnter2D(Collider2D trigg)
    {
        if (trigg.gameObject.CompareTag("ToMove"))// drag inca nerezolvat 
        {
            can_press = true;
            if (Input.GetButtonDown("Drag") == true)
                active_drag = 1;
        }
        if (trigg.gameObject.CompareTag("Trampoline"))
        {
            isGrounded = false;
            if (upSpeed < 30)// pentru a nu putea sari la infinit
                upSpeed += 5;
            trampoline_sound.Play();
            rb.velocity = Vector2.up * upSpeed;
        }
    }
    void OnTriggerStay2D(Collider2D trigg)
    {
        if (trigg.gameObject.CompareTag("Scara"))
        {
            rb.gravityScale = 0f;
            active_scara = 1;
        }
        if (trigg.gameObject.CompareTag("Chest"))
            chest = 1;

    }
    void OnTriggerExit2D(Collider2D trigg)
    {
        if (trigg.gameObject.CompareTag("ToMove"))
        {
            if (Input.GetButtonDown("Drag") == false)
                active_drag = 0;
        }
        if (trigg.gameObject.CompareTag("Scara"))
        {
            rb.gravityScale = 3f;
            active_scara = 0;
        }
        if (trigg.gameObject.CompareTag("Chest"))
            chest = 0;// daca nu am cheia, aici o sa punem canvasul cu cheia 'take the key and press O'
            
    }
    void OnCollisionEnter2D(Collision2D collision)// tot ceea ce este legat de collisions
    {
        if (collision.gameObject.CompareTag(GROUND_TAG))
            isGrounded = true;
        if (collision.gameObject.CompareTag("Boat") && boat == true)
            StartCoroutine(WaitBeforeTheBoatStarts());
        if (collision.gameObject.CompareTag(DESTROY_TAG))// distruem obiectul cu care interactioneaza
            Destroy(collision.gameObject);
        if (collision.gameObject.CompareTag(DESTROY_PLAYER_TAG)) // distrugem playerul 
            StartCoroutine(DestroyTime());
        if (collision.gameObject.CompareTag(DESTROY_PLAYER_INSTANTLY_TAG)) // distrugem playerul 
            DestroyInstantly();
        if (collision.gameObject.CompareTag("Key"))
        {
            collect_sound.Play();
            PlayerPrefs.SetInt("KEY", 1);
            //   system.Save();
            Destroy(collision.gameObject);
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Boat"))
            velocity = 0;
    }
    IEnumerator WaitBeforeTheBoatStarts()
    {
        yield return new WaitForSeconds(0.7f);
        velocity = 8f;
    }
    IEnumerator DestroyTime()
    {
        checkpoint = 1;
        anim.SetBool(DESTROY_ANIMATION, true);
        yield return new WaitForSeconds(0.8f);
        anim.SetBool(DESTROY_ANIMATION, false);
        SceneManager.LoadScene(2);
    }
    private void DestroyInstantly()
    {
        checkpoint = 1;
        SceneManager.LoadScene(2);
    }
}
