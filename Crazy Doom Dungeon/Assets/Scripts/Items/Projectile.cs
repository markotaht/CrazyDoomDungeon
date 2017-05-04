using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour {
    [SerializeField]
    private float speed;
    private Vector3 direction;
    private bool attacking = false;
    private Transform target;

    private Rigidbody rb;

    private float timetolive = 10;
	// Use this for initialization
	void Awake () {
        //rb = GetComponent<Rigidbody>();
        //rb.useGravity = true;
        //Destroy(gameObject, timetolive);
	}
    /*
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<Renderer>().material.color = Color.red;
            other.gameObject.GetComponent<BasicAI>().Die();
        }
    }
    */
    // Update is called once per frame
    void Update () {
        if (attacking)
        {
            Vector3 destination = target.position;
            if(Vector3.Distance(destination, transform.position) < 1)
            {
                target.GetComponent<BasicAI>().WasHit();
                Destroy(gameObject);
            }
            Vector3 direction = (target.position - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(direction);
            transform.position = transform.position + Time.deltaTime * speed * direction;
        }
	}

    public void setDirection(Vector3 direction)
    {
        this.direction = direction;

        float yOffset = 0;
        float angle = 5 * Mathf.Deg2Rad;
        float speed = (1/ Mathf.Cos(angle))* Mathf.Sqrt((0.5f * Physics.gravity.magnitude * Mathf.Pow(direction.magnitude, 2)) / (direction.magnitude * Mathf.Tan(angle) + yOffset));

        Vector3 velocity = new Vector3(0,speed*Mathf.Sin(angle), speed*Mathf.Cos(angle));
        Vector3 finalVel = Quaternion.LookRotation(direction)*velocity;
        rb.AddForce(finalVel*GetComponent<Rigidbody>().mass, ForceMode.Impulse);
    }

    public void attack(Transform newTarget)
    {
        target = newTarget;
        attacking = true;
    }
}
