using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot2 : MonoBehaviour {

    private Button button;
    private DatabaseItem item;

    public DatabaseItem Item
    {
        get
        {
            return item;
        }
    }

    public int count = 0;
	// Use this for initialization
	void Start () {
        button = GetComponent<Button>();
        button.onClick.AddListener(HandleClick);
	}
	
    public void AddItem(DatabaseItem item)
    {
        this.item = item;
        count = 1;
        if(item != null)
        {
            button.image.sprite = item.Sprite;
        }else
        {
            button.image.sprite = null;
            count = 0;
        }
       
    }

    public void IncreaseCount()
    {
        count++;
    }

    public void DecreaseCount()
    {
        count--;
    }

    public void HandleClick()
    {
        if (item != null)
        {
     //       ItemDescriptionGUI.instance.setItem(this);
        }
    }
}
