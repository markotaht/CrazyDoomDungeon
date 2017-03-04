using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    Vector3 target;

    [SerializeField]
    private float MoveSpeed = 6.0f;
    private bool isAttacking = false;
    // Use this for initialization
	void Start () {
        target = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if(Vector3.Distance(transform.position,target) > 0.7) { 
            Vector3 direction = target- transform.position;
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

    public void Attack(Vector3 target)
    {
        isAttacking = true;
        float dist = Vector3.Distance(transform.position, target);
        if (dist > 1.2)
        {
            Move(transform.position + (target-transform.position).normalized * (dist-1.2f));
        }
    }
}

