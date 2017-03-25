using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackContoller : MonoBehaviour {

    private bool isAttacking = false;

    private Vector3 target;
    private float range;
    private Weapon weapon;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isAttacking)
        {
            float dist = Vector3.Distance(transform.position, target);
            if(dist <= range)
            {
                Vector3 direction = target - transform.position;
                direction.y = 0;
                transform.rotation = Quaternion.LookRotation(direction.normalized);
                weapon.Attack(target - transform.position);
                isAttacking = false;
            }
        }
	}

    public void Attack(Vector3 target, Weapon weapon)
    {
        isAttacking = true;
        this.target = target;
        range = weapon.getRange();
        this.weapon = weapon;
        float dist = Vector3.Distance(transform.position, target);
        if (dist > range)
        {
            GetComponent<MovementController>().Move(transform.position + (target - transform.position).normalized * (dist-range+1));
        }
        else
        {
            Vector3 planarTarget = new Vector3(target.x, 0, target.z);
            Vector3 planarPosition = new Vector3(transform.position.x, 0, transform.position.z);
            Vector3 direction = planarTarget - planarPosition;
            transform.rotation = Quaternion.LookRotation(direction.normalized);
            weapon.Attack(direction);
        }
    }
}
