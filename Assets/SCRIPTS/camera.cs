using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class camera : MonoBehaviour
{
    public Transform target;
    [SerializeField] private float x_set, y_set, x_min, x_max;
    Vector3 temp;
    void Start()
    {
       
    }
    void LateUpdate()
    { 
        if (target)// in caz ca e distrus sa nu mai accesez transform  
        {
            temp = transform.position;
            temp.x = target.position.x;
            temp.y = target.position.y;
            if (temp.x > x_min && temp.x < x_max)
                transform.position = new Vector3(target.position.x + x_set, target.transform.position.y + y_set, transform.position.z);
        }

    }
}
