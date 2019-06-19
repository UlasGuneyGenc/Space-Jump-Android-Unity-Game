using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private int powerupID;
    GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        Destroy(this.gameObject,30.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.activeSelf == false)
        {
            Destroy(this.gameObject);
        }
        transform.Translate(Vector3.down * Time.deltaTime * 3.0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Controller player = other.GetComponent<Controller>();

            if (player != null)
            {
               
                if (powerupID == 0)
                {
                    player.DoubleCoin();
                }
                else if (powerupID == 1)
                {
                    player.Invisible();   
                }
                else if (powerupID == 2)
                {
                    player.Rocket();
                }
            }


            Destroy(this.gameObject);

        }
    }
}
