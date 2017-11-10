using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragItem : MonoBehaviour {
    private DatabaseItem item;
    public DatabaseItem Item
    {
        get
        {
            return item;
        }
        set
        {
            item = value;
            GetComponent<Image>().sprite = item.Sprite;
        }
    }
}
