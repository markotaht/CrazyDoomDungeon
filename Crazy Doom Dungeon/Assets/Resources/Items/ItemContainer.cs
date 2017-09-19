using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Xml.Serialization;
using System.IO;

[XmlRoot("itemCollection")]
public class ItemContainer : MonoBehaviour {

    [XmlArray("Items")]
    [XmlArrayItem("DatabaseItem")]
    public List<DatabaseItem> items = new List<DatabaseItem>();
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
