using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour {

    enum State { READY_TO_ATTACK, ATTACK_WINDUP, ATTACK_WINDDOWN };
    private State current_state = State.READY_TO_ATTACK;

    private Transform target;
    private float range;
    private Weapon weapon;
    
    private float attackWinddown;
    private float attackWindup;

    private float attackCountdown;
    
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        attackCountdown -= Time.deltaTime;
        switch (current_state)
        {
            case State.READY_TO_ATTACK:
                if (target != null && target.GetComponent<BasicAI>().isAlive())
                {
                    float dist = Vector3.Distance(transform.position, target.position);
                    if (dist > range)
                    {
                        GetComponent<MovementController>().Move(transform.position + (target.position - transform.position).normalized * (dist - range + 1));
                    }
                    else
                    {

                        //Stop and turn to enemy
                        GetComponent<MovementController>().Move(transform.position);
                        Vector3 planarTarget = new Vector3(target.position.x, 0, target.position.z);
                        Vector3 planarPosition = new Vector3(transform.position.x, 0, transform.position.z);
                        Vector3 direction = planarTarget - planarPosition;
                        transform.rotation = Quaternion.LookRotation(direction.normalized);

                        weapon.StartAttack(target);
                        attackCountdown = attackWindup;
                        current_state = State.ATTACK_WINDUP;
                    }
                }
                break;

            case State.ATTACK_WINDUP:
                if(attackCountdown <= 0)
                {
                    weapon.DoAttack(target);
                    attackCountdown = attackWinddown;
                    current_state = State.ATTACK_WINDDOWN;
                }
                break;

            case State.ATTACK_WINDDOWN:
                if(attackCountdown <= 0)
                {
                    target = null;
                    current_state = State.READY_TO_ATTACK;
                }
                break;
        }
	}

    public void Attack(Transform target, Weapon weapon)
    {
        switch (current_state)
        {
            case State.READY_TO_ATTACK:
                //start attacking new
                SetTargetAndWeapon(target, weapon);

                float dist = Vector3.Distance(transform.position, target.position);
                if (dist > range)
                {
                    current_state = State.READY_TO_ATTACK;
                    GetComponent<MovementController>().Move(transform.position + (target.position - transform.position).normalized * (dist - range + 1));
                }
                else
                {
                    current_state = State.ATTACK_WINDUP;

                    //Stop and turn to enemy
                    GetComponent<MovementController>().Move(transform.position);
                    Vector3 planarTarget = new Vector3(target.position.x, 0, target.position.z);
                    Vector3 planarPosition = new Vector3(transform.position.x, 0, transform.position.z);
                    Vector3 direction = planarTarget - planarPosition;
                    transform.rotation = Quaternion.LookRotation(direction.normalized);

                    weapon.StartAttack(target);
                    attackCountdown = attackWindup;
                }
                break;
                
            case State.ATTACK_WINDDOWN:
            case State.ATTACK_WINDUP:
                break;
        }
    }
    private void SetTargetAndWeapon(Transform target, Weapon weapon)
    {
        this.target = target;
        this.weapon = weapon;
        range = weapon.getRange();
        attackWindup = weapon.getWindupSpeed();
        attackWinddown = weapon.getWinddownSpeed();
    }
    
    public void StopAttacking()
    {
        switch (current_state)
        {
            case State.READY_TO_ATTACK:
                target = null;
                break;
                
            case State.ATTACK_WINDUP:
            case State.ATTACK_WINDDOWN:
                break;
        }
    }

    public bool canMove()
    {
        switch (current_state)
        {
            case State.READY_TO_ATTACK:
                return true;
                
            case State.ATTACK_WINDUP:
            case State.ATTACK_WINDDOWN:
                return false;
        }
        return false;
    }

    public bool isAttacking()
    {
        switch (current_state)
        {
            case State.READY_TO_ATTACK:
                return false;
                
            case State.ATTACK_WINDUP:
            case State.ATTACK_WINDDOWN:
                return true;
        }
        return false;
    }
    public Weapon getWep()
    {
        return weapon;
    }
}
