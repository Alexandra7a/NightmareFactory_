using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parent : MonoBehaviour
{
    public GameObject player;
    public static Vector3 pos;
    public SaveSystem system;
    void Start()
    {
        Transform ck = transform.Find("CheckPoints");
        foreach (Transform i in ck)
        {

            CheckPoint c = i.GetComponent<CheckPoint>();
            c.SetCheckPointNAME(this);
        }
    }
    public void Player_check(CheckPoint check)
    {
        PlayerPrefs.SetFloat("last_point_x", check.transform.position.x);
        PlayerPrefs.SetFloat("last_point_y", check.transform.position.y);
        system.Save();
    }
}


