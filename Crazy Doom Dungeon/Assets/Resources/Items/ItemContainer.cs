using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Xml.Serialization;
using System.IO;

public class ItemContainer{

    [XmlArray("Items")]
    [XmlArrayItem("DatabaseItem")]
    public Dictionary<long, DatabaseItem> itemsDB = new Dictionary<long, DatabaseItem>();
	
    public void Load(string path)
    {
        TextAsset _xml = Resources.Load<TextAsset>(path);

        XmlSerializer serializer = new XmlSerializer(typeof(ItemDirectory));

        StringReader reader = new StringReader(_xml.text);

        ItemDirectory items = serializer.Deserialize(reader) as ItemDirectory;

        reader.Close();

        for(int i = 0; i < items.items.Length; i++)
        {
            DatabaseItem di = items.items[i];
            itemsDB.Add(di.Id, di);
        }
    }
}


[XmlRoot("ItemCollection")]
public class ItemDirectory
{
    [XmlArray("Items")]
    [XmlArrayItem("DatabaseItem")]
    public DatabaseItem[] items;
}
