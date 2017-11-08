using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreGUI : MonoBehaviour {

    public static StoreGUI instance;

    [SerializeField]
    RectTransform shop;
    [SerializeField]
    RectTransform inventory;
    [SerializeField]
    RectTransform StoreInventory;


    private void Start()
    {
        instance = this;
     //   gameObject.SetActive(false);
        /*  foreach(Transform child in grid.transform)
          {
              Grids.Add(child.GetComponent<InventorySlot>());
          }*/
    }

    public void Toggle()
    {
        shop.gameObject.SetActive(!shop.gameObject.active);
        inventory.gameObject.SetActive(!inventory.gameObject.active);
    }

}
