using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IHasChanged:  IEventSystemHandler{
    void HasChanged();
    void HasChanged(bool primary);
}
