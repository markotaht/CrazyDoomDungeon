using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    Vector3 target;
    // Use this for initialization
	void Start () {
        target = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);

            target = hit.point;
        }
        if(Vector3.Distance(transform.position,target) > 1.1) { 
            Vector3 direction = target- transform.position;
            direction.y = 0;

            transform.rotation = Quaternion.LookRotation(direction.normalized);
            transform.Translate(Vector3.forward * Time.deltaTime * 6);
        }
        else
        {
            target = transform.position;
        }
       
    }
}
