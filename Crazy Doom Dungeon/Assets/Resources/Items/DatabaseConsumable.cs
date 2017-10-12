using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Xml;
using System.Xml.Serialization;

public class DatabaseConsumable : DatabaseItem{

    [XmlElement("HealthIncrease")]
    public float _healthIncrease;

    public override void Use()
    {
        GameObject go = GameObject.Find("Player 1(Clone)");
        go.GetComponent<PlayerController>().GiveHealth(_healthIncrease);
        go.GetComponent<Inventory>().RemoveItem(this);
    }
}
