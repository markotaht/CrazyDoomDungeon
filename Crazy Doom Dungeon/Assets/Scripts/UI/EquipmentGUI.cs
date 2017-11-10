using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentGUI : MonoBehaviour, IHasChanged {
    /*
    private bool EquipmentOn = false;

    [SerializeField]
    private GUISkin skin;


    private Vector2 WindowPos = new Vector2(0, 0);
    private Vector2 WindowSize = new Vector2(360, 360);

    private Vector2 ClosePos = new Vector2(312, 5);
    private Vector2 CloseSize = new Vector2(35, 35);

    private Vector2 NamePos = new Vector2(30, 30);
    private Vector2 NameSize = new Vector2(250, 250);

    [SerializeField]
    private Texture EquipmentWindow;
    [SerializeField]
    private Texture CloseIcon;

    [SerializeField]
    private GUIContent Head;
    [SerializeField]
    private Vector2 Headpos = new Vector2(160,60);
    [SerializeField]
    private GUIContent Body;
    [SerializeField]
    private Vector2 Bodypos = new Vector2(160, 150);
    [SerializeField]
    private GUIContent Legs;
    [SerializeField]
    private Vector2 Legspos = new Vector2(160, 240);
    [SerializeField]
    private GUIContent LeftHand;
    [SerializeField]
    private Vector2 LeftHandpos = new Vector2(80, 150);
    [SerializeField]
    private GUIContent RightHand;
    [SerializeField]
    private Vector2 RightHandpos = new Vector2(240, 150);

    private Rect DragWindowPos = new Rect(0, 0, 360, 360);
    // Use this for initialization
    void Start () {
        Head.text = "Head";
        Body.text = "Body";
        Legs.text = "Legs";
        LeftHand.text = "Left Hand";
        RightHand.text = "Right Hand";
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp("e"))
        {
            if(EquipmentOn == false)
            {
                EquipmentOn = true;
            }
            else if (EquipmentOn)
            {
                EquipmentOn = false;
            }
        }

        if(Input.GetKey("left shift"))
        {
            if (Input.GetKeyUp("e"))
            {
                DragWindowPos.x = 10;
                DragWindowPos.y = 10;
            }
        }
	}

    private void OnGUI()
    {
        GUI.skin = skin;
        if (EquipmentOn)
        {
            DragWindowPos = GUI.Window(1, DragWindowPos, EquipmentWindowFunc, "");
        }
    }

    private void EquipmentWindowFunc(int WindowId)
    {
        if (EquipmentOn)
        {
            GUI.BeginGroup(new Rect(WindowPos, WindowSize), EquipmentWindow);
            GUI.Label(new Rect(NamePos, NameSize), "Your Equipment");

            if (GUI.Button(new Rect(ClosePos, CloseSize), CloseIcon))
            {
                EquipmentOn = false;
            }
            GUI.Button(new Rect(Headpos, CloseSize), Head);
            GUI.Button(new Rect(Bodypos, CloseSize), Body);
            GUI.Button(new Rect(Legspos, CloseSize), Legs);
            GUI.Button(new Rect(LeftHandpos, CloseSize), LeftHand);
            GUI.Button(new Rect(RightHandpos, CloseSize), RightHand);

            GUI.DragWindow(new Rect(WindowPos.x, WindowPos.y, 10000, 40));
            GUI.EndGroup();
        }
    }*/

    public static EquipmentGUI instance;
    public EquipmentSlot primaryWeapon;
    public EquipmentSlot secondaryWeapon;
    public GameObject inventoryItemPefab;

    private void Awake()
    {
        instance = this;
    }

    public void AddPrimary(GameObject item)
    {
        GameObject temp = Instantiate(inventoryItemPefab, transform);
        temp.GetComponent<DragItem>().Item = item.GetComponent<Item>().getDBItem();
        temp.transform.SetParent(primaryWeapon.transform);
    }

    public void AddSecondary(GameObject item)
    {
        GameObject temp = Instantiate(inventoryItemPefab, transform);
        temp.GetComponent<DragItem>().Item = item.GetComponent<Item>().getDBItem();
        temp.transform.SetParent(secondaryWeapon.transform);
    }

    public void HasChanged()
    {
       // throw new NotImplementedException();
    }

    public void HasChanged(bool primary)
    {
       // throw new NotImplementedException();
    }
}
