using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Button yourButton1, yourButton2, yourButton3;
    public GameObject Transition;
    public GameObject ObjToHide;
    public SaveSystem system;// pentru salvarea datelor dupa iesirea din joc 
    void Awake()
    {
        ObjToHide.SetActive(true);
        system.Load();

        Transition.GetComponent<AudioSource>().Stop();
        //  Transition = GetComponent<Animator>();
    }
    public SavePositionManager save;
    void Start()
    {
        Button b1 = yourButton1.GetComponent<Button>();
        Button b2 = yourButton2.GetComponent<Button>();
        Button b3 = yourButton3.GetComponent<Button>();

        b1.onClick.AddListener(NewGame);
        b2.onClick.AddListener(Continue);
        b3.onClick.AddListener(Exit); 
    }
    //pentru butonul de new game avem tranzitia de inceput. Vom incarca doua scene in acelasi timp pentru a merge efectul de transparenta. Cand butonul e apasat, audio listenerul din first scene e dezactivat deoarece nu putem avea numai unul singur ruland. Cand dam pe butonul de continue, audioul va fi activat pentru a se putea auzi pasii
    void NewGame()
    {
        // Facem un coroutine pentru scena de GoodNight 
        system.DeleteSaveData();// delete data din save system 
        save.StartPosition();
        Transition.GetComponent<Animator>().SetTrigger("gNight_in");//pornim animatia de intrare
        StartCoroutine(GoodNight());
        //SceneManager.LoadScene(2);
    }
    
    IEnumerator GoodNight()
    {//incarcam tranzitia de la meniu la inceputul jocului
        Transition.GetComponent<AudioSource>().Play();//pornim sunetul
        yield return new WaitForSeconds(7);
        var async = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);//First
        if (!async.isDone)
        {
            yield return null;
        }
        ObjToHide.SetActive(false);
        Transition.GetComponent<Animator>().SetTrigger("gNight_out");// tranzitia de final 
    }
    void Continue()
    {
        if (system.activeSave.EXIT == 1)
            SceneManager.LoadScene(2);
        else
        {
            if (first_scene_player.firstOpening == 3) // daca intram prima data in joc sa nu putem apasa butonul de continua first va fi 3 doar dupa ce se executa first scene player 
            {
                //StartCoroutine(Transition());
                SceneManager.LoadScene(1);// save.Index returneaza scena la care se afla player-ul 
            }
            else
            {
                // cand se apasa butonul, acesta va deveni gri 
                var b2_color = yourButton2.GetComponent<Button>().colors;
                b2_color.normalColor = Color.gray;
                yourButton2.GetComponent<Button>().colors = b2_color;
            }
        }
    }
    void Exit()
    {
        //Play transition
        // system.Save();//Salveaza datele, astfel Exit o sa fie 1 si la revenire in joc se va putea apasa butonul continue
        Application.Quit();
    }
    /*void Transition(float index)
    {
        
        StartCoroutine(TransitionTime(SceneManager.GetActiveScene().buildIndex+1));
    }
    IEnumerator TransitionTime(int index)
    {
        Transition.SetTrrigger("start_animation");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(index);
    }*/
}
