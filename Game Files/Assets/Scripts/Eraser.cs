using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eraser : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Coin"))
        {

            Destroy(col.gameObject);
        }
        if (col.gameObject.CompareTag("Platform") || col.gameObject.CompareTag("RedPlatform"))
        {
            Destroy(col.gameObject);
        }
    }
}
