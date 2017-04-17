using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackContoller : MonoBehaviour {

    private bool isAttacking = false;

    private Transform target;
    private float range;
    private Weapon weapon;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isAttacking)
        {
            if (!target.gameObject.GetComponent<BasicAI>().isAlive())
            {
                Debug.Log("Target is dead, will stop attacking");
                isAttacking = false;
                return;
            }
            Debug.Log("attacking " + target);
            float dist = Vector3.Distance(transform.position, target.position);
            if(dist <= range)
            {
                Debug.Log("In range of " + target);
                weapon.getAnimator().SetBool("attacking", true);
                Vector3 direction = target.position - transform.position;
                direction.y = 0;
                transform.rotation = Quaternion.LookRotation(direction.normalized);
                weapon.Attack(target.position - transform.position);
                //Debug.Log("Stopping attacking " + target);
                //isAttacking = false;
            }
            else
            {
                Debug.Log("Not in range of " + target);
                GetComponent<MovementController>().Move(transform.position + (target.position - transform.position).normalized * (dist-range+1));
            }
        }
        else
        {
            if (weapon)
            {
                weapon.getAnimator().SetBool("attacking", false);
            }
        }
	}

    public void Attack(Transform target, Weapon weapon)
    {
        isAttacking = true;
        this.target = target;
        range = weapon.getRange();
        this.weapon = weapon;
        float dist = Vector3.Distance(transform.position, target.position);
        if (dist > range)
        {
            GetComponent<MovementController>().Move(transform.position + (target.position - transform.position).normalized * (dist-range+1));
        }
        else
        {
            weapon.getAnimator().SetBool("attacking", true);
            GetComponent<MovementController>().Move(transform.position);
            Vector3 planarTarget = new Vector3(target.position.x, 0, target.position.z);
            Vector3 planarPosition = new Vector3(transform.position.x, 0, transform.position.z);
            Vector3 direction = planarTarget - planarPosition;
            transform.rotation = Quaternion.LookRotation(direction.normalized);
            weapon.Attack(direction);
        }
    }
    
    public void StopAttacking()
    {
        isAttacking = false;
    }
}
