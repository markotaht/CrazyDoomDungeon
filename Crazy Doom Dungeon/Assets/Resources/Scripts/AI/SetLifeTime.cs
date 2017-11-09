using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLifeTime : MonoBehaviour {

    public float timer;
	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, timer);
	}

}
