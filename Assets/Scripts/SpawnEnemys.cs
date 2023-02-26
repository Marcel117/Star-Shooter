using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemys : MonoBehaviour
{
    public GameObject enemyPreFab;
    public float delaySpawn;
    public Transform leftLimit;
    public Transform rightLimit;
    private float minX;
    private float maxX;
    private float tempTime; // Temporizador
    void Start()
    {
        minX = leftLimit.position.x;
        maxX = rightLimit.position.x;
    }
    void Update()
    {
        tempTime += Time.deltaTime;
        if(tempTime >= delaySpawn) 
        {
            tempTime = 0;
            spawn();
        }
    }

    void spawn()
    {        
        float posX = Random.Range(minX, maxX);
        GameObject temp = Instantiate(enemyPreFab) as GameObject;
        temp.transform.position = new Vector3(posX, transform.position.y, transform.position.z);
    }
}
