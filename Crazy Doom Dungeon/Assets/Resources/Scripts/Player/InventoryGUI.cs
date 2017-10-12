using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

using UnityEngine.UI;

public class InventoryGUI : MonoBehaviour {

    public static InventoryGUI instance;

    public GameObject grid;

    public static Action<int> ItemAction;
    public List<InventorySlot> Grids = new List<InventorySlot>();

    private void Start()
    {
        instance = this;
        gameObject.SetActive(false);
        foreach(Transform child in grid.transform)
        {
            Grids.Add(child.GetComponent<InventorySlot>());
        }
    }

    public void Toggle()
    {
        this.gameObject.SetActive(!this.gameObject.active);
    }
    /*
    private bool InventoryOn = false;
    private Vector2 scrollBarChopGrid = Vector2.zero;
    private int GridValue = -1;

    public static Action<int> ItemAction;

    private int width = 6;
    private int height = 10;

    [SerializeField]
    private GUISkin skin;

    private Vector2 WindowPos = new Vector2(0, 0);
    private Vector2 WindowSize = new Vector2(360, 360);

    private Vector2 ClosePos = new Vector2(312, 5);
    private Vector2 CloseSize = new Vector2(35, 35);

    private Vector2 ScrollPos = new Vector2(0, 95);
    private Vector2 ScrollSize = new Vector2(353, 257);

    private Vector2 GridPos = new Vector2(10, 2);
    private Vector2 GridSize = new Vector2(323, 410);

    private Vector2 NamePos = new Vector2(30, 30);
    private Vector2 NameSize = new Vector2(250, 250);

    private Rect DragWindowPos = new Rect(0,0,360,360);

    [SerializeField]
    private Texture InventoryWindow;
    [SerializeField]
    private Texture CloseIcon;
    [SerializeField]
    private GUIContent[] Grids;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp("i"))
        {
            if(InventoryOn  == false)
            {
                InventoryOn = true;
            }
            else if (InventoryOn)
            {
                InventoryOn = false;
                scrollBarChopGrid.y = 0;
                GridValue = -1;
            }
        }

        if(Input.GetKey("left shift"))
        {
            if (Input.GetKeyUp("i"))
            {
                DragWindowPos.x = 10;
                DragWindowPos.y = 10;
            }
        }
	}

    private void OnGUI()
    {
        GUI.skin = skin;
        if (InventoryOn)
        {
            DragWindowPos = GUI.Window(0, DragWindowPos, InventoryWindowFunc, "");
        }
    }

    private void InventoryWindowFunc(int WindowId)
    {
        if (InventoryOn)
        {
            GUI.BeginGroup(new Rect(WindowPos, WindowSize), InventoryWindow);
            GUI.Label(new Rect(NamePos, NameSize), "Your Inventorys");

            if (GUI.Button(new Rect(ClosePos, CloseSize), CloseIcon))
            {
                InventoryOn = false;
            }

            scrollBarChopGrid = GUI.BeginScrollView(new Rect(ScrollPos, ScrollSize), scrollBarChopGrid,
                new Rect(0, 0, 0, 420));
            for(int y = 0; y < height; y++)
            {
                for(int x = 0; x < width; x++)
                {
                    Rect rect = new Rect(50*x+20,50*y,50,50);
                    bool slot = GUI.Button(rect, Grids[x+y*width]);

                    if (slot)
                    {
                        ItemAction(x + y * width);
                    }
                }
            }
            //GridValue = GUI.SelectionGrid(new Rect(GridPos, GridSize), GridValue, Grids, 5);
            GUI.EndScrollView();

            GUI.DragWindow(new Rect(WindowPos.x, WindowPos.y, 10000, 40));
            GUI.EndGroup();
        }
    }
    */
    public void AddItem(int index, DatabaseItem item)
    {
        Grids[index].AddItem(item);
        //Grids[index].image = item.sprite;
        //Grids[index].text = item.name;
        
    }

    public void IncreaseCount(int index)
    {
        Grids[index].IncreaseCount();
    }

    public void DecreaseCount(int index)
    {
        Grids[index].DecreaseCount();
    }

    public int getCount(int index)
    {
        return Grids[index].count;
    }

    public void RemoveItem(int index)
    {
        Grids[index].AddItem(null);
    //    Grids[index].image = null;
    //    Grids[index].text = null;
    }
    /*
    public void SetInventorySize(int size)
    {
        height = size / width;
        Grids = new GUIContent[size];
        for(int i = 0; i < size; i++)
        {
            Grids[i] = new GUIContent();
        }
    }
    */
}
