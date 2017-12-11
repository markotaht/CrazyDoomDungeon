using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDescriptionGUI : MonoBehaviour {

    public static ItemDescriptionGUI instance;

    public Image itemImage;
    public Text count;
    private DatabaseItem DBitem;
    [SerializeField]
    private Button useButton;

    private void Start()
    {
        instance = this;
    }

    public void setItem(GameObject item)
    {
        if (item != null)
        {
            DBitem = item.GetComponent<DragItem>().Item;
            itemImage.sprite = DBitem.Sprite;
            useButton.Select();
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
        InventoryGUI.instance.Select();
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
