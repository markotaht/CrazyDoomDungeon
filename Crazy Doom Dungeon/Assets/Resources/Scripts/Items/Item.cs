using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Item : MonoBehaviour {

    [SerializeField]
    public Sprite sprite;

    [SerializeField]
    protected int DatabaseID;

    public string name;

    protected DatabaseItem DBitem;

    private void Start()
    {
        if(DBitem == null)
        {
            DBitem = ItemLoader.instance.getItem(DatabaseID);
            name = DBitem.name;
            sprite = DBitem.Sprite;
        }
        StartItem();
    }

    protected abstract void StartItem();

    public abstract void Use();

}
