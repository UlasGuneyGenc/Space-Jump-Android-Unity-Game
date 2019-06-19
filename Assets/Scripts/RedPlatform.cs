using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPlatform : MonoBehaviour
{

    private Rigidbody2D rb;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb.velocity.y <= 0)
            {
                rb.velocity = new Vector2(0, 12.0f);
                Destroy(this.gameObject);
            }

        }
    }
}
