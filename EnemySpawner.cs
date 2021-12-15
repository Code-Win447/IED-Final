using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemy;
    public float xPos;
    public float zPos;
    public float yPos;
    public float enemyCount = 0f;
    public float spawnRate = 1f;
    public float salt;
    public Camera fps;
    

    

    private void Update()
    {
        StartCoroutine(Spawn());

    }
    private void Start()
    {
        //starts spawning enemies with randomness, to avoid errors

        GameObject player = GameObject.Find("Player");

        salt = Random.Range(0, 10);


        xPos = GameObject.Find("Spawner").transform.position.x + salt;

        salt = Random.Range(0, 10);

        yPos = GameObject.Find("Spawner").transform.position.y + salt;

        salt = Random.Range(0, 10);

        zPos = GameObject.Find("Spawner").transform.position.z + salt;
    }


    IEnumerator Spawn()
    {
        while (enemyCount < 10)
        {
            //tells the enemy to go towards the player when spawned
            
            GameObject playerA = GameObject.Find("Player");
            Transform playerT = playerA.GetComponent<Transform>();
            EnemyFollow enemyFollow = enemy.GetComponent<EnemyFollow>();
            enemyFollow.player = playerT;
            
            
            //spawns enemy
            Instantiate(enemy, new Vector3(xPos, yPos, zPos), Quaternion.identity);
            yield return new WaitForSeconds(spawnRate);
            enemyCount += 1;
            Rigidbody rb = enemy.GetComponent<Rigidbody>();
            rb.AddForce(10, 10, 10, ForceMode.Impulse);

            
        }
    }
}
