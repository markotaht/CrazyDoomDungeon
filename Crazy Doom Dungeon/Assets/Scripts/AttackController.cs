using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour {

    private bool attacking = false;

    private Transform target;
    private float range;
    private Weapon weapon;

    private float attackCountdown;
    private float attackCooldown;
    private bool inAttackAnim = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (attacking)
        {
            if (inAttackAnim)
            {
                attackCountdown -= Time.deltaTime;
                if(attackCountdown <= 0)
                {
                    bool killed = target.GetComponent<BasicAI>().WasHit();
                    attackCooldown = weapon.getAttackSpeed() - weapon.getWindupSpeed();
                    inAttackAnim = false;
                    if (killed)
                    {
                        attacking = false;
                        return;
                    }
                }
            }
            else
            {
                attackCooldown -= Time.deltaTime;
                float dist = Vector3.Distance(transform.position, target.position);
                if (dist <= range)
                {
                    if(attackCooldown <= 0)
                    {
                        weapon.Attack(target);
                        attackCountdown = weapon.getWindupSpeed();
                        inAttackAnim = true;
                    }
                }
                else
                {
                    GetComponent<MovementController>().Move(transform.position + (target.position - transform.position).normalized * (dist - range + 1));
                }
            }
        }
	}

    public void Attack(Transform target, Weapon weapon)
    {
        attacking = true;
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
            //Stop and turn to enemy
            GetComponent<MovementController>().Move(transform.position);
            Vector3 planarTarget = new Vector3(target.position.x, 0, target.position.z);
            Vector3 planarPosition = new Vector3(transform.position.x, 0, transform.position.z);
            Vector3 direction = planarTarget - planarPosition;
            transform.rotation = Quaternion.LookRotation(direction.normalized);
            
            weapon.Attack(target);
            attackCountdown = weapon.getWindupSpeed();
            inAttackAnim = true;
        }
    }
    
    public void StopAttacking()
    {
        attacking = false;
    }

    public bool isAttacking()
    {
        return attacking;
    }
    public Weapon getWep()
    {
        return weapon;
    }
}
