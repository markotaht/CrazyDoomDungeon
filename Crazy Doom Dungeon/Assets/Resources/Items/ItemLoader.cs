using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLoader : MonoBehaviour {

    public static ItemLoader instance;
    public const string path = "Items/items";

    public ItemContainer ic;

    void Awake()
    {
        instance = this;
    }

    void Start () {

        ic = new ItemContainer();
        ic.Load(path);
	}
	
    public DatabaseItem getItem(long id)
    {
        return ic.itemsDB[id];
    }
	
}
