﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentSlot : Slot {

    [SerializeField]
    public bool Primary = true;

    protected override bool SetItem(GameObject obj)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject weapon = obj.GetComponent<DragItem>().Item.GetModel(player.transform);
        weapon.GetComponent<BoxCollider>().enabled = false;
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
        return true;
    }

    public void SetItem(DatabaseWeapon wep)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject weapon = wep.GetModel(player.transform);
        weapon.GetComponent<BoxCollider>().enabled = false;
        if (Primary)
        {
            player.GetComponent<EquipmentHandler>().SetPrimary(weapon.GetComponent<Weapon>());
        }
        else
        {
            player.GetComponent<EquipmentHandler>().SetSecondary(weapon.GetComponent<Weapon>());
        }
    }
}
