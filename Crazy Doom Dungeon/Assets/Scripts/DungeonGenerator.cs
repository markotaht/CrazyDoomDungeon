﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class DungeonGenerator : MonoBehaviour {

    [SerializeField]
    private int iterations = 5;
    [SerializeField]
    private GameObject[] dungeonParts;
    [SerializeField]
    private GameObject[] rooms;
    private GameObject player;
    private List<GameObject> openExits = new List<GameObject>();

    private byte[,,] map = new byte[500,10,500];
    private Vector3 center = new Vector3(250, 5, 250);

    private Dictionary<string, string[]> rules = new Dictionary<string, string[]>()
    {
        {"Room", new string[] { "Corridor" } },
        {"Corridor", new string[] {"Room", "Junction"} },
        {"Junction", new string[] {"Corridor"} }
    };

    // Use this for initialization
    void Start () {
        dungeonParts = dungeonParts.Concat(rooms).ToArray();
        GameObject startDungeonPart = dungeonParts[Random.Range(0, dungeonParts.Length)];
        CreateDungeon(startDungeonPart, iterations);
        createNavMesh();
        AddPlayer();
        visualizeMap();
    }

    private void Update()
    {
        if (Input.GetButton("Restart"))
        {
            Application.LoadLevel("Diana");
        }
    }

    void visualizeMap()
    {
        GameObject Map = new GameObject();

        for(int x = 0; x < 500; x++)
        {
            for(int y = 0; y < 10; y++)
            {
                for(int z = 0; z < 500; z++)
                {
                    if (map[x, y, z] != 0)
                    {
                        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        cube.transform.position = new Vector3(x-150, y, z-250);
                        cube.transform.parent = Map.transform;
                    }
                }
            }
        }
    }
	

    void CreateDungeon(GameObject startDungeonPart, int iterations)
    {
        GameObject part = Instantiate(startDungeonPart, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        openExits = getExits(part);
        part.transform.parent = transform;
        AddToMap(part);
        while (iterations > 0)
        {
            List<GameObject> newExits = new List<GameObject>();
            foreach(GameObject exit in openExits)
            {
                List<GameObject> newModuleExits;
                GameObject chosenExit;
                while (true)
                {
                    GameObject newPart = ChooseNewPart(exit.transform.parent.tag);
                    newPart = Instantiate(newPart) as GameObject;
                    newPart.transform.parent = transform;
               
                    newModuleExits = getExits(newPart);
                    chosenExit = newModuleExits[Random.Range(0, newModuleExits.Count)];
                    MatchExits(exit, chosenExit);
                    AddToMap(newPart);
                    break;
                }
                foreach (GameObject new_exit in newModuleExits)
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
        CloseCorridors(openExits);
    }

    void CloseCorridors(List<GameObject> exits)
    {
        foreach(GameObject exit in exits)
        {
            if(exit.transform.parent.tag == "Junction")
            {
                //TODO Valida corridor ja sinna otsa panna room
                GameObject newPart = dungeonParts[Random.Range(0, rooms.Length)];
                while(newPart.tag != "Corridor")
                {
                    newPart = dungeonParts[Random.Range(0, rooms.Length)];
                }
                newPart = Instantiate(newPart) as GameObject;
                newPart.transform.parent = transform;
                List<GameObject> newModuleExits = getExits(newPart);
                int index = Random.Range(0, newModuleExits.Count);
                GameObject chosenExit = newModuleExits[index];
                MatchExits(exit, chosenExit);
                AddToMap(newPart);

                GameObject otherExit = newModuleExits[(index + 1) % 2];
                GameObject newPart2 = rooms[Random.Range(0, rooms.Length)];
                newPart2 = Instantiate(newPart2) as GameObject;
                newPart2.transform.parent = transform;
                List<GameObject> newModuleExits2 = getExits(newPart2);
                GameObject chosenExit2 = newModuleExits2[Random.Range(0, newModuleExits2.Count)];
                MatchExits(otherExit, chosenExit2);
                AddToMap(newPart2);
            }
            else if(exit.transform.parent.tag == "Corridor")
            {
                GameObject newPart = rooms[Random.Range(0, rooms.Length)];
                newPart = Instantiate(newPart) as GameObject;
                newPart.transform.parent = transform;
                List<GameObject> newModuleExits = getExits(newPart);
                GameObject chosenExit = newModuleExits[Random.Range(0, newModuleExits.Count)];
                MatchExits(exit, chosenExit);
                AddToMap(newPart);
            }
        }
    }

    GameObject ChooseNewPart(string oldPart)
    {
        GameObject newPart;
        do
        {
            newPart = dungeonParts[Random.Range(0, dungeonParts.Length)];
        } while (!rules[oldPart].Any(tag => tag == newPart.tag));
        return newPart;
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

    bool CheckOverlap(GameObject part)
    {
        List<Vector3> coords = part.GetComponent<GenerateArray>().getArea();
        for (int i = 0; i < coords.Count; i++)
        {
            Vector3 point = coords[i];
            int y = (int)(point.y / 4 + center.y);
            if(map[(int)(point.x + center.x), y, (int)(point.z + center.z)]!= 0) return true;
        }
        return false;
    }

    void AddToMap(GameObject part)
    {
        List<Vector3> coords = part.GetComponent<GenerateArray>().getArea();
        byte id = part.GetComponent<GenerateArray>().getId();

        for (int i = 0; i < coords.Count; i++)
        {
            Vector3 point = coords[i];
            int y = (int)(point.y / 4 + center.y) ;
            map[(int)(point.x+ center.x), y, (int)(point.z + center.z)] = id;
        }
    }

    float Azimuth(Vector3 vector)
    {
        return Vector3.Angle(Vector3.forward, vector) * Mathf.Sign(vector.x);
    }

    void AddPlayer()
    {
        player = Resources.Load<GameObject>("Characters/Capsule");
        player = Instantiate(player, new Vector3(0, 0.75f, 1.24f), Quaternion.identity);
        InputHandler handler = gameObject.AddComponent(typeof(InputHandler)) as InputHandler;
        handler.setPlayer(player.GetComponent<PlayerController>());
    //    var pos = player.transform.position;
    //    pos.y = transform.position.y;
        Camera camera = Camera.main;
        CameraFollow follow = camera.gameObject.AddComponent(typeof(CameraFollow)) as CameraFollow;
        follow.setTarget(player.transform);

    //    camera.GetComponent<Clipper>().player = player.transform;
    //    camera.GetComponent<CameraFollow>().setTarget(player.transform);
      /*  camera.transform.position = new Vector3(-10, 10, -10);
        camera.transform.parent = player.transform;
        camera.transform.Rotate(new Vector3(30, 45, 0));
        camera.orthographic = true;
        camera.orthographicSize = 5;*/
    }

    void createNavMesh()
    {
        NavMeshSurface[] surfaces = GetComponents<NavMeshSurface>();

        for(int i = 0; i < surfaces.Length; i++)
        {
            surfaces[i].BuildNavMesh();
        }
        Spawner.spawn = true;
    }

}
