using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using UnityEngine.SceneManagement;

public class DungeonGenerator : MonoBehaviour {

    [SerializeField]
    private int iterations = 5;
    [SerializeField]
    private GameObject[] dungeonParts;
    [SerializeField]
    private GameObject[] rooms;
    private GameObject player;
    private List<GameObject> usedParts = new List<GameObject>();
    private List<GameObject> enemies = new List<GameObject>();
    private List<GameObject> openExits = new List<GameObject>();
    private UIController uicontroller;

    private byte[,,] map = new byte[500,10,500];
    private Vector3 center = new Vector3(250, 5, 250);

    private bool navmesh = false;
    private bool hide = false;
    private Dictionary<string, string[]> rules = new Dictionary<string, string[]>()
    {
        {"Room", new string[] { "Corridor" } },
        {"Corridor", new string[] {"Room", "Junction"} },
        {"Junction", new string[] {"Corridor"} }
    };

    // Use this for initialization
    void Start () {
        uicontroller = GameObject.FindObjectOfType<UIController>() as UIController;
        dungeonParts = dungeonParts.Concat(rooms).ToArray();
        GameObject startDungeonPart = dungeonParts[Random.Range(0, dungeonParts.Length)];
        CreateDungeon(startDungeonPart, iterations);

        
       // visualizeMap();
    }

    private void Update()
    {
        if (Input.GetButton("Restart"))
        {
            Time.timeScale = 1;
            //uicontroller.ShowLoading(true);
            SceneManager.LoadScene("Diana");
        }
        if (!navmesh)
        {
            createNavMesh();
            AddPlayer();
            //     visualizeMap();
            navmesh = true;
            //uicontroller.ShowLoading(false);
        }else if (!hide)
        {
            hideElements(player.transform.position.y+2);
            hide = true;
        }
    }

    public void hideElements(float y)
    {
        for (int i = 0; i < usedParts.Count; i++)
        {
            if (usedParts[i].transform.position.y > y)
            {
                usedParts[i].SetActive(false);
            }
            else
            {
                usedParts[i].SetActive(true);
            }
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].transform.position.y > y)
            {
                enemies[i].SetActive(false);
            }
            else
            {
                enemies[i].SetActive(true);
            }
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

    bool CreateDungeon(GameObject prefab, int depth, GameObject exit = null)
    {   
        GameObject part = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
        List<GameObject> exits = getExits(part);
        if (exit)
        { 
            List<GameObject> unTestedExits = getExits(part);
            bool fits = false;
            while (!fits) {
                if (unTestedExits.Count == 0) break;

                GameObject chosenExit = unTestedExits[Random.Range(0, unTestedExits.Count)];
                unTestedExits.Remove(chosenExit);
                   
                MatchExits(exit, chosenExit);
                if (CheckFit(part))
                {
                    AddToMap(part);
                    exits.Remove(chosenExit);
                    fits = true;
                }
            }
            if (!fits)
            {
                DestroyObject(part);
                return false;
            }
        }else
        {
            AddToMap(part);
            usedParts.Add(part);
        }
        bool anymatch = false;
        while (depth >= 0 && exits.Count > 0)
        {
            GameObject nextExit = exits[Random.Range(0, exits.Count)];
            exits.Remove(nextExit);
            List<GameObject> possible = dungeonParts.Where(c => rules[part.tag].Any(tag => tag == c.tag)).ToList();
            while (possible.Count > 0)
            {
                GameObject newPart = possible[Random.Range(0, possible.Count)];
                possible.Remove(newPart);
                if (CreateDungeon(newPart, depth - 1, nextExit))
                {
                    anymatch = true;
                    break;
                }
            }
        }
        if (!anymatch && part.tag != "Room")
        {
            DestroyObject(part);
            RemoveFromMap(part);
            return false;
        }
        usedParts.Add(part);
        part.transform.parent = transform;
        return true;
    }
/*
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
                bool match = false;
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
*/
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
        newPart.transform.RotateAround(exit2.transform.position, Vector3.up, Mathf.Round(correctiveRotation));
        Vector3 correctiveTranslation = exit1.transform.position - exit2.transform.position;
        newPart.transform.position += correctiveTranslation;
    }
    
    void AddToMap(GameObject part)
    {
        List<Vector3> coords = part.GetComponent<GenerateArray>().getArea();
        byte id = part.GetComponent<GenerateArray>().getId();

        for (int i = 0; i < coords.Count; i++)
        {
            Vector3 point = coords[i];
            int y = (int)(point.y / 8 + center.y);
            map[(int)(point.x+ center.x), y, (int)(point.z + center.z)] = id;
        }
    }

    void RemoveFromMap(GameObject part)
    {
        List<Vector3> coords = part.GetComponent<GenerateArray>().getArea();
        byte id = part.GetComponent<GenerateArray>().getId();

        for (int i = 0; i < coords.Count; i++)
        {
            Vector3 point = coords[i];
            int y = (int)(point.y / 8 + center.y);
            map[(int)(point.x + center.x), y, (int)(point.z + center.z)] = 0;
        }
    }

    bool CheckFit(GameObject part)
    {
        List<Vector3> coords = part.GetComponent<GenerateArray>().getArea();
        for (int i = 0; i < coords.Count; i++)
        {
            Vector3 point = coords[i];
            int x = (int)(point.x + center.x);
            int y = (int)(point.y / 8 + center.y);
            int z = (int)(point.z + center.z);
            if (map[x, y, z] != 0) return false;
        }
        return true;
    }

    float Azimuth(Vector3 vector)
    {
        return Vector3.Angle(Vector3.forward, vector) * Mathf.Sign(vector.x);
    }

    void AddPlayer()
    {
        player = Resources.Load<GameObject>("Characters/Player 1");
        player = Instantiate(player, new Vector3(0, 1.1f, 1.24f), Quaternion.identity);
        player.GetComponent<AttackController>().attackCooldownImage = uicontroller.GetAttackCooldownImage();
        player.GetComponent<AttackController>().swapCooldownImage = uicontroller.GetSwapCooldownImage();
        //InputHandler handler = gameObject.AddComponent(typeof(InputHandler)) as InputHandler;
        InputHandler handler = InputHandler.instance;
        handler.setPlayer(player.GetComponent<PlayerController>());

        AInputHandler handler2 = ControllerInputHandler.instance;
        handler2.setPlayer(player.GetComponent<PlayerController>());
        //    var pos = player.transform.position;
        //    pos.y = transform.position.y;
        Camera camera = Camera.main;
        CameraFollow follow = camera.gameObject.AddComponent(typeof(CameraFollow)) as CameraFollow;
        follow.setTarget(player.transform);

     //   camera.GetComponent<Clipper>().Player = player.transform;
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

    public void AddEnemy(GameObject enemy)
    {
        enemies.Add(enemy);
    }

}
