using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Settings : MonoBehaviour
{
    public AudioMixer mixer;
    public Image inactive_menu;
    private Animator anim;
    public SavePositionManager save;
    public SaveSystem system;
    // variabile
    [HideInInspector] public bool paused = false;//pentru a verifica in alte scripuri daca jocul este pe pauza
    int esc;
    public void SetLevel(float mix)
    {
        mixer.SetFloat("Volume", Mathf.Log10(mix) * 20);
    }
    public Button yourButton1, yourButton2, yourButton3, yourButton4;

    void Start()
    {
        esc = 0;
        anim = GetComponent<Animator>();
        Button b1 = yourButton1.GetComponent<Button>();
        b1.onClick.AddListener(TaskOnClick);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            esc++;
            TaskOnClick();
        }
        if (esc == 2)
        {
            esc = 0;
            Resume();
        }

    }
    void TaskOnClick()
    {
        Time.timeScale = 0f;//oprim jocul 
        paused = true;// anuntam si pentru player script ca jocul a fost oprit pentru a nu mai putea accesa input commands
        inactive_menu.gameObject.SetActive(true);// afisam meniul de pauza 
        Button b2 = yourButton2.GetComponent<Button>();// luam butoanele care sunt in meniul de pauza (b2,b3,b4)
      //  Button b3 = yourButton3.GetComponent<Button>();
        Button b4 = yourButton4.GetComponent<Button>();
        b2.onClick.AddListener(Resume);
      //  b3.onClick.AddListener(Setting);
        b4.onClick.AddListener(ExitToMainMenu);
    }
    void Resume()
    {
        Time.timeScale = 1f;
        inactive_menu.gameObject.SetActive(false);
        paused = false;
    }
    void Setting()
    {
        //Debug.Log("am apasat");
    }
    void ExitToMainMenu()
    {
        //trebuie sa vedem in ce scena suntem pentru a vedea daca putem salva datele in XML FILE 
        Scene currentScene = SceneManager.GetActiveScene();
        int Index = currentScene.buildIndex;
        if (Index == 2)// daca suntem la scena 2 atunci salvam datele 
            system.Save();
        Time.timeScale = 1f;// daca nu ai apasat pe resume o sa ramana blocat jocul la revenirea in scena de aceea inainte de a o parasi facem 1f
        save.Save();
        SceneManager.LoadScene(0);
    }
}
