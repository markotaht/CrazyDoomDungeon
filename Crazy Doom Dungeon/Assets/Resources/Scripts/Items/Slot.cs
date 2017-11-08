using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Slot : MonoBehaviour, IDropHandler, IPointerClickHandler{
    public GameObject item
    {
        get
        {
            if(transform.childCount > 0)
            {
                return transform.GetChild(0).gameObject;
            }
            return null;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!item)
        {
            SetItem(DragHandler.draggingObject);
            DragHandler.draggingObject.transform.SetParent(transform);
            ExecuteEvents.ExecuteHierarchy<IHasChanged>(gameObject,null,(x,y) => x.HasChanged());
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Here");
        ItemDescriptionGUI.instance.setItem(item);
    }

    protected abstract void SetItem(GameObject obj);
}
