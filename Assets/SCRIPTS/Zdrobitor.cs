using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zdrobitor : MonoBehaviour
{
    public float speed;
    private int tavan;
    void Start()
    {
        tavan = 1;
    }
    void LateUpdate()
    {
        if (tavan == 0)
            StartCoroutine(Urcare());
        else
            transform.position = new Vector2(transform.position.x, transform.position.y - (speed+Random.Range(1,6)) * Time.deltaTime);

    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Tavan"))
            tavan = 1;
        if (coll.gameObject.CompareTag("Ground"))
            tavan = 0;
    }
    IEnumerator Urcare()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime);
        yield return new WaitForSeconds(0.3f);
    }
}
