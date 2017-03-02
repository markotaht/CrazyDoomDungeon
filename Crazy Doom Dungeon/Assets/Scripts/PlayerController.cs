using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        var x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * 6;
        var z = Input.GetAxisRaw("Vertical") * Time.deltaTime * 6;

        Vector3 direction = Quaternion.AngleAxis(0,Vector3.up) * new Vector3(x, 0, z);
        
        transform.Translate(direction);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        transform.LookAt(new Vector3(hit.point.x,transform.position.y,hit.point.z));

	}
}
