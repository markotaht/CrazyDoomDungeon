using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot2 : MonoBehaviour {

    private Button button;
    private DatabaseItem item;

    public GameObject player;

    // Use this for initialization
    void Start () {
        button = GetComponent<Button>();
        button.onClick.AddListener(HandleClick);

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddItem(DatabaseItem item)
    {
        this.item = item;
        player = GameObject.FindGameObjectWithTag("Player");
        GameObject weapon = item.GetModel(player.transform);
   //     player.GetComponent<EquipmentHandler>().SetWeapon(weapon.GetComponent<Weapon>());
        if (item != null)
        {
            button.image.sprite = item.Sprite;
        }
        else
        {
            button.image.sprite = null;
        }

    }

    public void HandleClick()
    {
        if (item != null)
        {
           // ItemDescriptionGUI.instance.setItem(this);
        }
    }
}
