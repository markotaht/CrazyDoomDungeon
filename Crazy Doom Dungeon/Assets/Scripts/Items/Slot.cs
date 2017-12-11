using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class Slot : Selectable, IDropHandler, IPointerClickHandler, ISubmitHandler{
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
        if (!item && SetItem(DragHandler.draggingObject))
        {
            DragHandler.draggingObject.transform.SetParent(transform);
            ExecuteEvents.ExecuteHierarchy<IHasChanged>(gameObject,null,(x,y) => x.HasChanged());
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ItemDescriptionGUI.instance.setItem(item);
    }

    public void OnSubmit(BaseEventData eventData)
    {
        ItemDescriptionGUI.instance.setItem(item);
    }

    protected abstract bool SetItem(GameObject obj);
}
