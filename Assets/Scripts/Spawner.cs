using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] powerUps;
    public GameObject player;
    public Vector2 position;
    public GameObject player_;
    void Start()
    {

        player_ = GameObject.FindWithTag("Player");
        StartCoroutine(powerUpSpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (player.activeSelf == false)
        {
            Destroy(this.gameObject);
        }
    }

    public IEnumerator powerUpSpawnRoutine()
    {
        while (true) {
            position = new Vector2(player.transform.position.x, player.transform.position.y + 30);
            transform.position = position;
            int randomPowerup = Random.Range(0, 3);
            Instantiate(powerUps[randomPowerup], position, Quaternion.identity);
            yield return new WaitForSeconds(30.0f);
        }
    }

}
