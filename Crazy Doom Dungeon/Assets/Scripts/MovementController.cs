using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

    private Vector3 target;

    [SerializeField]
    private float MoveSpeed = 6.0f;

    private float constant;
    private bool isAttacking = false;
    // Use this for initialization
    void Start () {
        target = transform.position;
        constant = transform.position.y;
    }
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(transform.position, target) > constant)
        {
            Vector3 direction = target - transform.position;
            direction.y = 0;

            transform.rotation = Quaternion.LookRotation(direction.normalized);
            transform.Translate(Vector3.forward * Time.deltaTime * MoveSpeed);
        }
        else
        {
            target = transform.position;
            if (isAttacking)
            {
                //TODO play attack animation
                isAttacking = false;
            }
        }
    }

    public void Move(Vector3 target)
    {
        this.target = target;
    }
}
