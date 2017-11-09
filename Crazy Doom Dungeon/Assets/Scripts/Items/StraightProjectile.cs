using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightProjectile : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float damage;
    [SerializeField]
    private float maxDistance;
    //[SerializeField]
    //private Vector3 rotation;

    private Vector3 direction;
    private bool attacking = false;
    private float distanceTravelled = 0;
    //private Transform target;

    //private Rigidbody rb;

    //private float timetolive = 10;

    // Use this for initialization
    void Awake()
    {
        attacking = false;
        distanceTravelled = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (attacking && distanceTravelled < maxDistance)
        {
            distanceTravelled += Time.deltaTime * speed * 10;
            /*Vector3 destination = target.position;
            if (Vector3.Distance(destination, transform.position) < 1)
            {
                target.GetComponent<BasicAI>().WasHit(damage);
                Destroy(gameObject);
            }
            Vector3 direction = (target.position - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(direction);*/
            transform.position = transform.position + Time.deltaTime * speed * direction;
            //Debug.Log(distanceTravelled);
        }
        else if (distanceTravelled >= maxDistance)
        {
            Destroy(gameObject);
        }
    }
    /*
    public void setDirection(Vector3 direction)
    {
        this.direction = direction;

        float yOffset = 0;
        float angle = 5 * Mathf.Deg2Rad;
        float speed = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * Physics.gravity.magnitude * Mathf.Pow(direction.magnitude, 2)) / (direction.magnitude * Mathf.Tan(angle) + yOffset));

        Vector3 velocity = new Vector3(0, speed * Mathf.Sin(angle), speed * Mathf.Cos(angle));
        Vector3 finalVel = Quaternion.LookRotation(direction) * velocity;
        //rb.AddForce(finalVel * GetComponent<Rigidbody>().mass, ForceMode.Impulse);
    }
    */
    public void attack(Vector3 targetPos)
    {
        //target = newTarget;
        //targetPos.y = transform.position.y;
        attacking = true;
        direction = (targetPos - transform.position).normalized;
        direction.y = 0;
        direction = direction.normalized;
    }

    private void OnTriggerEnter(Collider other)
    {
        ABaseAI enemy = other.GetComponent<ABaseAI>();
        if (enemy)
        {
            enemy.WasHit(damage);
        }
    }
}
