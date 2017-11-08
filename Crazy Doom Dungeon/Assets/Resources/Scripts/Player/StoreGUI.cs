using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreGUI : MonoBehaviour, IHasChanged {

    public static StoreGUI instance;

    [SerializeField]
    RectTransform shop;
    [SerializeField]
    RectTransform inventory;
    [SerializeField]
    RectTransform StoreInventory;

    [SerializeField]
    GameObject itemPrefab;


    private void Start()
    {
        instance = this;
        foreach(Transform t in StoreInventory)
        {
            GameObject go = Instantiate(itemPrefab);
            go.transform.SetParent(t);
            go.GetComponent<DragItem>().Item = ItemLoader.instance.getItem(3);
        }
    }

    public void Toggle()
    {
        shop.gameObject.SetActive(!shop.gameObject.active);
        inventory.gameObject.SetActive(!inventory.gameObject.active);
    }

    public void HasChanged()
    {
      
    }

    public void HasChanged(bool primary)
    {
       
    }
}
