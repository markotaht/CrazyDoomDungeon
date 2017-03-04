using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour {
    private int iterations;
    private GameObject[] dungeonParts;
    private GameObject player;

    private float trackingSpeed = 2.0f;
    private float zoomSpeed = 5.0f;

    // Use this for initialization
    void Start () {
        dungeonParts = Resources.LoadAll<GameObject>("DungeonParts");
        GameObject dungeonPart = dungeonParts[Random.Range(0, dungeonParts.Length)];
        AddDungeonPart(dungeonPart);
        AddPlayer();
    }
	

    void AddDungeonPart(GameObject dungeonPart)
    {
        GameObject part = Instantiate(dungeonPart, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
    }

    void AddPlayer()
    {
        player = Resources.Load<GameObject>("Characters/Player");
        player = Instantiate(player, new Vector3(0, 0.75f, 1.24f), Quaternion.identity);
        var pos = player.transform.position;
        pos.y = transform.position.y;
        Camera camera = Camera.main;
        camera.transform.position = new Vector3(-10, 10, -10);
        camera.transform.parent = player.transform;
        camera.transform.Rotate(new Vector3(30, 45, 0));
        camera.orthographic = true;
        camera.orthographicSize = 5;
    }

}
