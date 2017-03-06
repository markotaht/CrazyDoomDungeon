using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackContoller : MonoBehaviour {

    private bool isAttacking = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Attack(Vector3 target, Weapon weapon)
    {
        isAttacking = true;
        float dist = Vector3.Distance(transform.position, target);
        if (dist > weapon.getRange())
        {
            GetComponent<MovementController>().Move(transform.position + (target - transform.position).normalized * (dist - 1.2f));
        }
        else
        {
            weapon.Attack((target - transform.position).normalized);
        }
    }
}
