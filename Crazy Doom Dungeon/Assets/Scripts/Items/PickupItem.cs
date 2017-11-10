using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickupItem : Item {

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<Inventory>().AddItem(DatabaseID);
            Destroy(this.gameObject);
        }
    }
}
