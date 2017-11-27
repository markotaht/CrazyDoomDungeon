using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DropController))]
public class HitableObject : MonoBehaviour {

    DropController dc;

    // Use this for initialization
    void Start () {
        dc = GetComponent<DropController>();
	}

    private void OnTriggerEnter(Collider other)
    {
        dc.DropSomething();
        GetComponent<BoxCollider>().enabled = false;
        Destroy(gameObject);
    }
}
