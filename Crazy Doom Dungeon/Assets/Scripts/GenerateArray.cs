using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateArray : MonoBehaviour {

    private List<Vector3> coveredArea;
	void Start () {
       
    }

    void findArea()
    {
        coveredArea = new List<Vector3>();
        int childrencount = transform.childCount;
        for(int i = 0; i < childrencount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            if(child.tag != "Exit")
            {
                Vector3 size = child.GetComponent<Renderer>().bounds.size;
                Vector3 offset = child.transform.position;

                for(int x = 0; x < size.x; x++)
                {
                    for(int y = 0; y < size.y; y++)
                    {
                        for(int z = 0; z< size.z; z++)
                        {
                            coveredArea.Add(new Vector3(x + offset.x, y + offset.y, z + offset.z));
                        }
                    }
                }
            }
        }
    }
	
    public List<Vector3> getArea()
    {
        findArea();
        return coveredArea;
    }
}
