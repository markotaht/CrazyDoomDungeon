using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateArray : MonoBehaviour {

    [SerializeField]
    private byte id = 0;

    private List<Vector3> coveredArea;
	void Start () {
       
    }

    public byte getId() { return id; }

    void findArea()
    {
        coveredArea = new List<Vector3>();
        int childrencount = transform.childCount;
        for(int i = 0; i < childrencount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            if(child.tag != "Exit" && child.tag != "Spawner")
            {
               
                Vector3 size = child.GetComponent<Renderer>().bounds.size;
                if (name == "AngledCorridor 1(Clone)")
                {
                    Debug.Log(size);
                }
                Vector3 offset = child.transform.position-size/2;

                for(int x = 0; x < size.x; x++)
                {
                    for(int y = 0; y < Mathf.Ceil(size.y); y++)
                    {
                        for(int z = 0; z< size.z; z++)
                        {
                            coveredArea.Add(new Vector3(x + offset.x, y + offset.y, z + offset.z));
                        }
                    }
                }
            }
        }
        if (name == "AngledCorridor 1(Clone)")
        {
            Debug.Log(coveredArea.Count);
        }
    }
	
    public List<Vector3> getArea()
    {
        findArea();
        return coveredArea;
    }
}
