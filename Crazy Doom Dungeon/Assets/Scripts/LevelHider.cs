using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHider : MonoBehaviour {

    DungeonGenerator gen;
	// Use this for initialization
	void Start () {
        gen = GameObject.FindObjectOfType<DungeonGenerator>();
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            gen.hideElements(transform.position.y); 
        }
    }


}
