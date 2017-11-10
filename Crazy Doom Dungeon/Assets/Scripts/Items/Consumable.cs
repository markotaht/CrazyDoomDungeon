using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : PickupItem {

    private float HealthIncrease;

    protected override void StartItem()
    {
        HealthIncrease = ((DatabaseConsumable)DBitem)._healthIncrease;
    }

    public  override void Use()
    {
        GameObject go = GameObject.Find("Player 1(Clone)");
        go.GetComponent<PlayerController>().GiveHealth(HealthIncrease);
    }
}
