using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemies;
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
        while (true)
        {
            position = new Vector2(player.transform.position.x, player.transform.position.y + 30);
            transform.position = position;
            int randomEnemy = Random.Range(0, 2);
            Instantiate(enemies[randomEnemy], position, Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
    }

}
