using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour {

    private Vector3 direction;

    [SerializeField]
    private float Speed = 2.0f;

    private Rigidbody rb;

    private float timetolive = 10;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        Destroy(gameObject, timetolive);
	}

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
    }

    // Update is called once per frame
    void Update () {

	}

    public void setDirection(Vector3 direction)
    {
        this.direction = direction;
        GetComponent<Rigidbody>().AddForce(this.direction*10f*Speed, ForceMode.Impulse);
    }
}
