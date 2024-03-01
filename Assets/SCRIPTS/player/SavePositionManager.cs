using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavePositionManager : MonoBehaviour
{
    public static float x, y;
    public static int INDEX;
    Vector3 pos,pos2;
    public static Vector3 pos_initiala, pos_portal;
    public static int NewButtonPressed = 0, PortalLevel1 = 0, ContinueButtonPressed,checkpoint=0;
    void Start()
    {
        //pozitiile player-ului din first scene 
        pos_initiala.x = 12.18f;
        pos_initiala.y = 2.67f;
        pos_portal.x = 127.68f;
        pos_portal.y = 1260.57f;
        PlayerPrefs.SetFloat("pos_initiala.x", pos_initiala.x);
        PlayerPrefs.SetFloat("pos_initiala.y", pos_initiala.y);
        PlayerPrefs.SetFloat("pos_portal.x", pos_portal.x);
        PlayerPrefs.SetFloat("pos_portal.y", pos_portal.y);
    }
    public void Save()
    {
        x = transform.position.x;
        y = transform.position.y;
        Scene current_scene = SceneManager.GetActiveScene();
        INDEX = current_scene.buildIndex;
        PlayerPrefs.SetFloat("x", x);
        PlayerPrefs.SetFloat("y", y);
    }
    public void Load()
    {
        x = PlayerPrefs.GetFloat("x");
        y = PlayerPrefs.GetFloat("y");
        pos = transform.position;
        pos.x = x;
        pos.y = y;
        transform.position = pos;
    }
    public int IndexScene()
    {
        INDEX = PlayerPrefs.GetInt("INDEX");
        return INDEX;
    }
    public void StartPosition()
    {
        NewButtonPressed++;// apelam startPos din MainMenu => 1, dupa care din first player script si o sa fie 2 
        if (NewButtonPressed == 2)// daca ambele apelari au avut loc atunci pozitia player-ului va fi cea de inceput 
        {
            pos_initiala.x = PlayerPrefs.GetFloat("pos_initiala.x");
            pos_initiala.y = PlayerPrefs.GetFloat("pos_initiala.y");
            pos = transform.position;
            pos.x = pos_initiala.x;
            pos.y = pos_initiala.y;
            transform.position = pos;
            NewButtonPressed = 0;//reinitializam variabila 
        }

    }
    public void Portal()
    {
        PortalLevel1=1;
    }
}
