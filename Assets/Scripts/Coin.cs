using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Coin : MonoBehaviour
{
    [SerializeField]
    private GameObject coin;
    

   private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject.Find("Player").GetComponent<Controller>().increaseCoin();
        Destroy(coin);
        
    }
}
