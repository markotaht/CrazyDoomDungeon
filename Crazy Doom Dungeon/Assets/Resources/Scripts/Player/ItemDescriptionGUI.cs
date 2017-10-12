using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDescriptionGUI : MonoBehaviour {

    public static ItemDescriptionGUI instance;

    public Image itemImage;
    public Text count;
    private InventorySlot DBitem;

    private void Start()
    {
        instance = this;
    }

    public void setItem(InventorySlot item)
    {
        DBitem = item;
        if (item != null)
        {
            itemImage.sprite = item.Item.Sprite;
            count.text = item.count.ToString();
        }else
        {
            itemImage.sprite = null;
            count.text = "0";
        }
    }

    public void Use()
    {
        DBitem.Item.Use();
        count.text = DBitem.count.ToString();
        if(DBitem.count == 0)
        {
            setItem(null);
        }
    }

    public void Drop()
    {

    }
}
