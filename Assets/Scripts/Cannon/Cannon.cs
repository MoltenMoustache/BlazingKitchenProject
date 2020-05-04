﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class Cannon : MonoBehaviour
{

    public Transform spawnPos;
    public GameObject fireball;
    public bool stopSpawning = false;
    public float spawnTime;
    public float spawnDelay;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObject", spawnTime, spawnDelay);
    }



    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
           // Instantiate(fireball, spawnPos.position, spawnPos.rotation);
        }
    }


    public void SpawnObject()
    {
        Instantiate(fireball, spawnPos.position, spawnPos.rotation);
        if(stopSpawning)
        {
            CancelInvoke("SpawnObject");
        }
    }
    

}


