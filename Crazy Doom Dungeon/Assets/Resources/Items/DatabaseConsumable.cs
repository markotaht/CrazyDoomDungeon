﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Xml;
using System.Xml.Serialization;
using System;

public class DatabaseConsumable : DatabaseItem{

    [XmlElement("HealthIncrease")]
    public float _healthIncrease;

    public override GameObject GetModel(Transform parent)
    {
        throw new NotImplementedException();
    }

    public override void Use()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Player");
        go.GetComponent<PlayerController>().GiveHealth(_healthIncrease);
    //    go.GetComponent<Inventory>().RemoveItem(this);
    }
}
