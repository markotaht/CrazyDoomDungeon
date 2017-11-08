using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentSlot : Slot {

    [SerializeField]
    public bool Primary = true;

    protected override void SetItem(GameObject obj)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject weapon = obj.GetComponent<DragItem>().Item.GetModel(player.transform);
        EquipmentSlot prevSlot = obj.transform.parent.GetComponent<EquipmentSlot>();
        if (Primary)
        {
            player.GetComponent<EquipmentHandler>().SetPrimary(weapon.GetComponent<Weapon>());
            if(prevSlot != null)
            {
                player.GetComponent<EquipmentHandler>().SetSecondary(null);
            }
        }else
        {
            player.GetComponent<EquipmentHandler>().SetSecondary(weapon.GetComponent<Weapon>());
            if(prevSlot != null)
            {
                player.GetComponent<EquipmentHandler>().SetPrimary(null);
            }
        }
    }
}
