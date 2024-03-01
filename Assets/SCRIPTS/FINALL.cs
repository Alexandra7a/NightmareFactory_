using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class FINALL : MonoBehaviour
{ 
    public Button yourButton1,yourButton2;
    void Start()
    {
        GetComponent<Animator>().SetTrigger("fade_gMorning");// tranzitia de final
        SceneManager.UnloadSceneAsync(2);
        Button b1=yourButton1.GetComponent<Button>();
        Button b2=yourButton2.GetComponent<Button>();

        b1.onClick.AddListener(GoToMainMenu);
        b2.onClick.AddListener(Exit);
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
