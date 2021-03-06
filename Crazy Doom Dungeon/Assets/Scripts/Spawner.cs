﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    [SerializeField]
    private GameObject item;
    public int maxAmount;
    public bool spawnOnce = true;
    public float spawnTimer = 1f;

    private float last_spawned = 0;
    private int spawned;

    public static bool spawn = false;

	// Use this for initialization
	void Awake () {
       
	}
	
	// Update is called once per frame
	void Update () {
        if (spawn)
        {
            int amount = Random.Range(1, maxAmount);
            for (int i = 0; i < amount; i++)
            {
                GameObject obj = Instantiate(item, transform.position + Vector3.up, Quaternion.Euler(0, Random.Range(0, 360), 0));
                transform.parent.parent.SendMessage("AddEnemy", obj);
            }
            enabled = false;
        }
	}
}
