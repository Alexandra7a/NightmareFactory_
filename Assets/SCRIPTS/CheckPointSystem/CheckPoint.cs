using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private Parent parent;
    void OnTriggerEnter2D(Collider2D trigg)
    {
        if (trigg.gameObject.CompareTag("Player"))
        {
            parent.Player_check(this);
        }
    }
    public void SetCheckPointNAME(Parent parent)
    {
        this.parent = parent;
    }
}
