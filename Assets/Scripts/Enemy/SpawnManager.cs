using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab1;
    public GameObject enemyPrefab2;
    public GameObject enemyPrefab3;


    public float spawnInterval = 5f;

    private Transform skeletonSpawner;
    private Transform spiderSpawner;
    private Transform cactusSpawner;

    private int enemyCount = 0;

    void Start()
    {
        skeletonSpawner = GameObject.Find("SkeletonSpawner").transform;
        cactusSpawner = GameObject.Find("CactusSpawner").transform;
        spiderSpawner = GameObject.Find("SpiderSpawner").transform;

        StartCoroutine(SpawnNPC());
    }

    IEnumerator SpawnNPC()
    {
        while (enemyCount < 5)
        {
            Vector3 spawnPosition1 = skeletonSpawner.position;
            GameObject npcInstance1 = Instantiate(enemyPrefab1, spawnPosition1, Quaternion.identity);

            Vector3 spawnPosition2 = cactusSpawner.position;
            GameObject npcInstance2 = Instantiate(enemyPrefab2, spawnPosition2, Quaternion.identity);

            Vector3 spawnPosition3 = spiderSpawner.position;
            GameObject npcInstance3 = Instantiate(enemyPrefab3, spawnPosition3, Quaternion.identity);


            enemyCount++;

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}