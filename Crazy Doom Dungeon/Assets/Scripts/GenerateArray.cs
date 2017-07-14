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
        if (transform.rotation.eulerAngles.y * 1000000 % 10000 != 0)
        {
            Debug.Log(transform.rotation.eulerAngles.y * 1000000 % 10000);
        }
        for(int i = 0; i < childrencount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            if(child.tag == "Floor")
            {
               
                Vector3 size = child.GetComponent<Renderer>().bounds.size;
                Vector3 offset = child.transform.position-size/2;
                for (int y = 0; y < size.y; y++)
                {
                    for (int x = 0; x < size.x; x++)
                    {
                        for (int z = 0; z< size.z; z++)
                        {
                            coveredArea.Add(new Vector3(x + offset.x, y+offset.y , z + offset.z));
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
