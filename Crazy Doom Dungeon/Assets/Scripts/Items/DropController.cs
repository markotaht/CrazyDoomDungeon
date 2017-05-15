using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropController : MonoBehaviour {
    
    [SerializeField]
    private GameObject[] drops;
    [SerializeField]
    private float[] droprate;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DropSomething()
    {
        for (int i = 0; i < drops.Length; i++)
        {
            float rnd = Random.Range(0.0f, 1.0f);
            if(rnd <= droprate[i])
            {
                Instantiate(drops[i], transform.position, Quaternion.identity);
            }
        }
    }
}
