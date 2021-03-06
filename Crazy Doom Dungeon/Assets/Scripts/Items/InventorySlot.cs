﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : Slot {
    protected override bool SetItem(GameObject obj)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        EquipmentSlot prevSlot = obj.transform.parent.GetComponent<EquipmentSlot>();
        if (prevSlot != null)
        {
            if(prevSlot.Primary)
                player.GetComponent<EquipmentHandler>().SetPrimary(null);
            else
                player.GetComponent<EquipmentHandler>().SetSecondary(null);
            return true;
        }
        StoreSlot storeSlot = obj.transform.parent.GetComponent<StoreSlot>();
        if(storeSlot != null)
        {
            int value = obj.GetComponent<DragItem>().Item.value;
            if(value <= Inventory.Currency)
            {
                Inventory.ChangeMoneyAmount(-value);
                return true;
            }
        }
        return false;
    }

}
