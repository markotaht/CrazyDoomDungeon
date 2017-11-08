using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Xml;
using System.Xml.Serialization;
using System;

public class DatabaseWeapon : DatabaseItem {

    [XmlElement("Damage")]
    public float damage;

    public override GameObject GetModel(Transform parent)
    {
        return GameObject.Instantiate(Model, parent);
    }

    public override void Use()
    {
   //     EquipmentGUI w = GameObject.FindObjectOfType<EquipmentGUI>();
   //     w.AddPrimary(this);
    }
}
