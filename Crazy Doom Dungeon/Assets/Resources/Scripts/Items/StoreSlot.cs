using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreSlot : Slot
{
    protected override bool SetItem(GameObject obj)
    {
        Inventory.ChangeMoneyAmount(obj.GetComponent<DragItem>().Item.value);
        return true;
    }
}
