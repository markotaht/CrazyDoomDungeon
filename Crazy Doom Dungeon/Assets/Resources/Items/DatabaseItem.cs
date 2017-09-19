using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Xml;
using System.Xml.Serialization;

public class DatabaseItem{

    [XmlAttribute("title")]
    public string title;

    [XmlAttribute("damage")]
    public float damage;
}
