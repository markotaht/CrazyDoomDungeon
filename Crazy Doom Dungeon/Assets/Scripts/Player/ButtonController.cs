using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour {

    [SerializeField]
    InventoryGUI inventory;
    [SerializeField]
    StoreGUI store;

    bool inventoryOpen = false;
    bool storeOpen = false;
	
    public void ToggleInventory()
    {
        if (storeOpen)
        {
            store.Toggle();
            storeOpen = false;
        }
        inventoryOpen = !inventoryOpen;
        inventory.Toggle();
    }

    public void ToggleStore()
    {
        if (inventoryOpen)
        {
            inventory.Toggle();
            inventoryOpen = false;
        }
        storeOpen = !storeOpen;
        store.Toggle();
    }
}
