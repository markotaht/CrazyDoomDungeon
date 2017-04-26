using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    [SerializeField]
    private GameObject item;

    [SerializeField]
    private int maxAmount;

    [SerializeField]
    private bool spawnOnce = true;

    [SerializeField]
    private float spawnTimer = 1f;

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
            int amount = Random.RandomRange(1, maxAmount);
            for (int i = 0; i < amount; i++)
            {
                Instantiate(item, transform.parent.position + Vector3.up, Quaternion.Euler(0, 0, 0));
            }
            enabled = false;
        }
	}
}
