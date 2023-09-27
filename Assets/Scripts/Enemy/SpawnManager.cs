using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab1;
    public GameObject enemyPrefab2; 
    public GameObject enemyPrefab3;
    public GameObject enemyPrefab4;
    public GameObject enemyPrefab5;



    public float spawnInterval = 5f;

    void Start()
    {
        StartCoroutine(SpawnNPC());
    }
    IEnumerator SpawnNPC()
    {
        while (true)
        {
            Vector3 spawnPosition1 = new Vector3(10f, 0f, 0f);
            GameObject npcInstance1 = Instantiate(enemyPrefab1, spawnPosition1, Quaternion.identity);


            Vector3 spawnPosition2 = new Vector3(20f, 0f, 0f);
            GameObject npcInstance2 = Instantiate(enemyPrefab2, spawnPosition2, Quaternion.identity);

            Vector3 spawnPosition3 = new Vector3(30f, 0f, 0f);
            GameObject npcInstance3 = Instantiate(enemyPrefab3, spawnPosition3, Quaternion.identity);

            Vector3 spawnPosition4 = new Vector3(40f, 0f, 0f);
            GameObject npcInstance4 = Instantiate(enemyPrefab4, spawnPosition4, Quaternion.identity);

            Vector3 spawnPosition5 = new Vector3(50f, 0f, 0f);
            GameObject npcInstance5 = Instantiate(enemyPrefab5, spawnPosition5, Quaternion.identity);


            yield return new WaitForSeconds(spawnInterval);

        }







    }
}