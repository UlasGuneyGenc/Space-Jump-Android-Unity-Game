using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Replace : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private GameObject[] platforms;
    private float maxY;
    [SerializeField]
    private GameObject Coin;
    int coinCD = 0;
    Vector3 location;
    public bool temp = true;
    public GameObject redP;
    public Transform transform1;


    private void Update()
    {
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Platform") || col.gameObject.CompareTag("RedPlatform"))
        {
            platforms = GameObject.FindGameObjectsWithTag("Platform");
            foreach (GameObject currentPlatform in platforms)
            {
                float currentMaxY = currentPlatform.gameObject.transform.position.y;
                if (currentMaxY > maxY)
                {
                    maxY = currentMaxY;
                }

            }
            col.gameObject.transform.position = new Vector2(Random.Range(-2.5f, 1.8f), maxY + Random.Range(0.7f, 2.5f));
            location = new Vector3(Random.Range(-2.5f, 1.8f), maxY, 0);//coinle ilgili olaylar

            if (coinCD == 0)
            {
                Instantiate(Coin, location, Quaternion.identity);
            }
            coinCD++;
            if (coinCD == 5)
            {
                coinCD = 0;
            }

        }
        if (col.gameObject.CompareTag("Coin"))
        {
            
            Destroy(col.gameObject);
        }
    }



   
}
