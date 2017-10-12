using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Xml;
using System.Xml.Serialization;
using System;

public class DatabaseWeapon : DatabaseItem {

    [XmlElement("Damage")]
    public float damage;

    public override void Use()
    {
        throw new NotImplementedException();
    }
}
