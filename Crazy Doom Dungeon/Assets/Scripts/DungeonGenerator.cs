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
        InputHandler handler = gameObject.AddComponent(typeof(InputHandler)) as InputHandler;
        handler.setPlayer(player.GetComponent<PlayerController>());
    //    var pos = player.transform.position;
    //    pos.y = transform.position.y;
        Camera camera = Camera.main;
        CameraFollow follow = camera.gameObject.AddComponent(typeof(CameraFollow)) as CameraFollow;
        follow.setTarget(player.transform);
    //    camera.GetComponent<CameraFollow>().setTarget(player.transform);
      /*  camera.transform.position = new Vector3(-10, 10, -10);
        camera.transform.parent = player.transform;
        camera.transform.Rotate(new Vector3(30, 45, 0));
        camera.orthographic = true;
        camera.orthographicSize = 5;*/
    }

}
