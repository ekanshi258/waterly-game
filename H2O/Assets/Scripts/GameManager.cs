﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject pureWater;
    public GameObject pollutedWater;
    public float startTime;
    public float repeatRate;
    public float ySpawnCoordinate;
    public int waterPoints = 0;
    public int healthPoints = 50;
    public Slider waterPointsSlider;
    public Slider energyPointsSlider;
    public float energyTime;
    public Bucket player;
    public Animator gameOverAnimation;

    private float timer;
    private GameObject spawnedObjects;
    private float minimumXSpawnCoordinate, maximumXSpawnCoordinate;

    private void Start()
    {
        Input.multiTouchEnabled = true;

        spawnedObjects = new GameObject("Enemies Parent");
        
        InvokeRepeating("SpawnWater", startTime, repeatRate);
        //InvokeRepeating("Spawn PowerUps", powerUpsStartTime, Random.Range(powerUpsSpawnRepeat - 5, powerUpsSpawnRepeat + 5));

        float halfHeight = (ySpawnCoordinate + ySpawnCoordinate) / 2.0f;
        maximumXSpawnCoordinate = (halfHeight * Screen.width / Screen.height) - 0.5f;
        minimumXSpawnCoordinate = -maximumXSpawnCoordinate;
    }

    private void Update()
    {
        waterPointsSlider.value = waterPoints;
        energyPointsSlider.value = healthPoints;

        waterPoints = Mathf.Min(waterPoints, 100);
        healthPoints = Mathf.Min(healthPoints, 100);

        if(waterPoints < 0)
        {
            waterPoints = 0;
        }
        if (healthPoints <= 0)
        {
            gameOverAnimation.SetTrigger("Game Over");
        }

        timer += Time.deltaTime;

        player.velocity = (healthPoints * 0.0008f) + 0.02f;

        repeatRate -= Time.deltaTime / 2f;
        Mathf.Clamp(repeatRate, 1.5f, 0.75f);
    }

    private void SpawnWater()
    {
        float i = Random.Range(0f, 10f);

        if (i < 6.5f)
            InstantiatePureWater();
        else
            InstantiatePollutedWater();
    }

    private void InstantiatePureWater()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(minimumXSpawnCoordinate, maximumXSpawnCoordinate), ySpawnCoordinate, 0f);
        Instantiate(pureWater, spawnPosition, Quaternion.identity, spawnedObjects.transform);
    }

    private void InstantiatePollutedWater()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(minimumXSpawnCoordinate, maximumXSpawnCoordinate), ySpawnCoordinate, 0f);
        Instantiate(pollutedWater, spawnPosition, Quaternion.identity, spawnedObjects.transform);
    }
}
