using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private Transform boxPrefab;
    [SerializeField] private int maxSpawnTimer;
    
    private float spawnTimer;
    private Vector3 randomPosition;
    void Start()
    {
        spawnTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        CheckSpawnTimer();
    }

    private void CheckSpawnTimer()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= maxSpawnTimer)
        {
            spawnTimer = 0f;
            SpawnBox();
        }
    }

    private void SpawnBox()
    {
        var box = Instantiate(boxPrefab);
        box.transform.position = RandomPosition();

    }

    private Vector3 RandomPosition()
    {
        var border = 10f;
        var randomPos = Random.Range(-border, border);

        var randomIndex = Random.Range(0, 4);
        switch (randomIndex)
        {
            case 0:
                randomPosition = new Vector3(randomPos, 0f, border);
                break;
            case 1:
                randomPosition = new Vector3(border, 0f, randomPos);
                break;
            case 2:
                randomPosition = new Vector3(randomPos, 0f, -border);
                break;
            case 3:
                randomPosition = new Vector3(-border, 0f, randomPos);
                break;
        }

        return randomPosition;
    }
}
