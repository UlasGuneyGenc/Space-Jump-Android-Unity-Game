using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameObject player;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        
    }

    // Update is called once per frame
    void Update()
    {

        if (player.activeSelf == false)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Controller player = other.GetComponent<Controller>();
            if (!player.isRocket || !player.isInvis)
            {
                Destroy(this.gameObject);
                if (player.isInDanger == false)
                {
                    player.isInDanger = true;
                    return;
                }
                if (player.isInvis)
                {
                    player.isInDanger = false;
                    return;
                }
                else if(player.isInDanger == true)
                {
                    player.noLife = true;
                    return;
                }
            }

        } 
    }
}
