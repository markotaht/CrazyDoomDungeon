using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightedRandomSelectList<T> : List<ListElement<T>> {

    private float totalWeight = 0;

    public void Add(T item, float weight)
    {
        totalWeight += weight;
        ListElement<T> elem = new ListElement<T>(item, weight);
        base.Add(elem);
    }

    public void Remove(T item)
    {
        int index = base.FindIndex(elem => elem.item.Equals(item));
        ListElement<T> element = base[index];
        totalWeight -= element.weight;
        base.Remove(element);
    }

    public T getRandom()
    {
        float weight = Random.Range(0.0f, totalWeight);
        float runningWeight = 0;
        for(int i = 0; i < base.Count; i++)
        {
            runningWeight += base[i].weight;
            if(runningWeight>= weight)
            {
                return base[i].item;
            }
        }
        return base[base.Count - 1].item;
    }
}

public class ListElement<T>
{
    public float weight;
    public T item;

    public ListElement(T item, float weight)
    {
        this.weight = weight;
        this.item = item;
    }
}
