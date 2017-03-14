using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour {

    [SerializeField]
    private int iterations = 5;
    private GameObject[] dungeonParts;
    private GameObject player;
    private List<GameObject> openExits = new List<GameObject>();

    private float trackingSpeed = 2.0f;
    private float zoomSpeed = 5.0f;

    // Use this for initialization
    void Start () {
        dungeonParts = Resources.LoadAll<GameObject>("DungeonParts");
        GameObject startDungeonPart = dungeonParts[Random.Range(0, dungeonParts.Length)];
        CreateDungeon(startDungeonPart, iterations);
        AddPlayer();
    }
	

    void CreateDungeon(GameObject startDungeonPart, int iterations)
    {
        GameObject part = Instantiate(startDungeonPart, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        openExits = getExits(part);
        while(iterations > 0)
        {
            List<GameObject> newExits = new List<GameObject>();
            foreach(GameObject exit in openExits)
            {
                GameObject newPart = dungeonParts[Random.Range(0, dungeonParts.Length)];
                newPart = Instantiate(newPart) as GameObject;
                List<GameObject> newModuleExits = getExits(newPart);
                GameObject chosenExit = newModuleExits[Random.Range(0, newModuleExits.Count)];
                MatchExits(exit, chosenExit);
                foreach(GameObject new_exit in newModuleExits)
                {
                    if(new_exit != chosenExit)
                    {
                        newExits.Add(new_exit);
                    }
                }
            }
            openExits = newExits;
            iterations--;
        }
    }

    List<GameObject> getExits(GameObject part)
    {
        List<GameObject> exits = new List<GameObject>();
        for (int i = 0; i < part.transform.childCount; i++)
        {
            if (part.transform.GetChild(i).tag == "Exit")
            {
                exits.Add(part.transform.GetChild(i).gameObject);
            }
        }
        return exits;
    }

    void MatchExits(GameObject exit1, GameObject exit2)
    {
        GameObject newPart = exit2.transform.parent.gameObject;
        Vector3 forwardToMach = -exit1.transform.forward;
        float correctiveRotation = Azimuth(forwardToMach) - Azimuth(exit2.transform.forward);
        newPart.transform.RotateAround(exit2.transform.position, Vector3.up, correctiveRotation);
        Vector3 correctiveTranslation = exit1.transform.position - exit2.transform.position;
        newPart.transform.position += correctiveTranslation;
    }

    float Azimuth(Vector3 vector)
    {
        return Vector3.Angle(Vector3.forward, vector) * Mathf.Sign(vector.x);
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
