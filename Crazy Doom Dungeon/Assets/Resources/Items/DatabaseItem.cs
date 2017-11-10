using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Xml;
using System.Xml.Serialization;

[XmlInclude(typeof(DatabaseWeapon))]
[XmlInclude(typeof(DatabaseConsumable))]
public abstract class DatabaseItem{

    [XmlAttribute("id")]
    public long Id;

    [XmlElement("Name")]
    public string name;

    [XmlElement("Sprite")]
    public string sprite_name;

    [XmlElement("Model")]
    public string model_name;

    [XmlElement("Stackable")]
    public bool stackable;

    [XmlElement("Value")]
    public int value;

    private Sprite _sprite;
    public Sprite Sprite
    {
        get
        {
            if(_sprite == null)
            {
                _sprite = Resources.Load<Sprite>("Sprites/" + sprite_name);
            }
            return _sprite;
        }
    }

    private GameObject _model;
    public GameObject Model
    {
        get
        {
            if(_model == null)
            {
                _model = Resources.Load<GameObject>("Prefabs/" + model_name);
            }
            return _model;
        }
    }

    public abstract void Use();

    public abstract GameObject GetModel(Transform parent);
}
