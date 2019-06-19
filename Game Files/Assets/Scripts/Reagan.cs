using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reagan : MonoBehaviour
{
    private bool teleported = false;
    private Transform playerTransform;
    public float speed=1;
   
    private void Update()
    {
        if (GameObject.Find("Player").GetComponent<Controller>().isInDanger)  //isindanger true ise çalışıyor
        {
            if (teleported == false)
            {
                playerTransform = GameObject.Find("Player").GetComponent<Transform>();
                transform.position = new Vector2(transform.position.x, playerTransform.position.y-7);
                this.transform.position = transform.position;
                teleported = true;
                StartCoroutine(TeleportSet());
            }
            else if (teleported)
            {
                transform.Translate(Vector3.up * Time.deltaTime * 2.8f *speed);
                
            }
        }
        else
        {//KANKA BURDA GERİ GİTME MUHABBETİ
          //  transform.Translate(Vector3.down * Time.deltaTime * 0.75f);
        }
        if (GameObject.Find("Player").GetComponent<Controller>().noLife)
        {
            if (teleported == false)
            {
                playerTransform = GameObject.Find("Player").GetComponent<Transform>();
                if (!((playerTransform.position.y - transform.position.y) < 7))
                {
                    transform.position = new Vector2(transform.position.x, playerTransform.position.y - 7);
                }
                this.transform.position = transform.position;
                teleported = true;
                StartCoroutine(TeleportSet());
            }
            if(teleported)
            {
                transform.Translate(Vector3.up * Time.deltaTime * 2.8f * 4);

            }
        }

        
    }

    IEnumerator TeleportSet()
    {
        yield return new WaitForSeconds(8);
        teleported = false;
    }


    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
           
            GameObject.Find("Player").GetComponent<Controller>().fallState();
            this.gameObject.SetActive(false);
        }
    }

}
