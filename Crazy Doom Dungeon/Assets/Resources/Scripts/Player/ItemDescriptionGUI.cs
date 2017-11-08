using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDescriptionGUI : MonoBehaviour {

    public static ItemDescriptionGUI instance;

    public Image itemImage;
    public Text count;
    private DatabaseItem DBitem;

    private void Start()
    {
        instance = this;
    }

    public void setItem(GameObject item)
    {
        
        DBitem = item.GetComponent<DragItem>().Item;
        if (item != null)
        {
            itemImage.sprite = DBitem.Sprite;
        //    count.text = item.count.ToString();
        }else
        {
            itemImage.sprite = null;
        //    count.text = "0";
        }
    }

    public void Use()
    {
        DBitem.Use();
    //    count.text = DBitem.count.ToString();
    //    if(DBitem.count == 0)
    //    {
            setItem(null);
    //    }
    }

    public void Drop()
    {

    }
}
