using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackController : MonoBehaviour {

    enum State { READY_TO_ATTACK, ATTACKING };
    private State current_state = State.READY_TO_ATTACK;

    private float range;
    private Weapon weapon;

    private float attackCooldown;

    private float attackCountdown;
    
    private List<Collider> CloseEnemies;
    private float viewcone = 80;
    private Transform target;
    public Image attackCooldownImage;
    public Image swapCooldownImage;
    public Button attackButton;
    public Button swapButton;
    private float swapCooldown = 0.5f;
    private float swapCooldownCounter = 0;


    // Use this for initialization
    void Start () {
        CloseEnemies = new List<Collider>();
	}
	
	// Update is called once per frame
	void Update () {
        //Check if target is in view, find new target if needed
        if(target == null || !IsInView(target) || !target.GetComponent<ABaseAI>().isAlive())
        {
            if(target != null)
            {
                target.GetComponent<ABaseAI>().Target(false);
            }
            target = null;
            FindNewTarget();
        }


        //Old stuff
        attackCountdown -= Time.deltaTime;
        swapCooldownCounter -= Time.deltaTime;
        switch (current_state)
        {
            case State.READY_TO_ATTACK:
                swapCooldownImage.fillAmount = (swapCooldownCounter / swapCooldown);
                if (swapCooldownCounter <= 0)
                {
                    swapButton.interactable = true;
                }
                break;
            case State.ATTACKING:
                attackCooldownImage.fillAmount = (attackCountdown / attackCooldown);
                swapCooldownImage.fillAmount = (attackCountdown / attackCooldown);
                if(attackCountdown <= 0)
                {
                    swapButton.interactable = true;
                    attackButton.interactable = true;
                    current_state = State.READY_TO_ATTACK;
                }
                break;
        }

    }

    public void Attack(Weapon weapon)
    {
        if (weapon == null) return;
        switch (current_state)
        {
            case State.READY_TO_ATTACK:
                {
                    SetTargetAndWeapon(target, weapon);
                    
                    current_state = State.ATTACKING;
                    
                    weapon.StartAttack(target);
                    attackCountdown = attackCooldown;
                    swapButton.interactable = false;
                    attackButton.interactable = false;
                }
                break;
                
            case State.ATTACKING:
                break;
        }

    }

    internal void SwapOnCooldown()
    {
        swapButton.interactable = false;
        swapCooldownCounter = swapCooldown;
    }

    public void Attack(Transform target, Weapon weapon)
    {
    }
    private void SetTargetAndWeapon(Transform target, Weapon weapon)
    {
        if (weapon == null) return;
        this.weapon = weapon;
        range = weapon.getRange();
        attackCooldown = weapon.getCooldown();
    }
    
    public void StopAttacking()
    {
        switch (current_state)
        {
            case State.READY_TO_ATTACK:
                target = null;
                break;
                
            case State.ATTACKING:
                break;
        }
    }

    public bool canMove()
    {
        switch (current_state)
        {
            case State.READY_TO_ATTACK:
                return true;
                
            case State.ATTACKING:
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
                
            case State.ATTACKING:
                return true;
        }
        return false;
    }
    public Weapon getWep()
    {
        return weapon;
    }

    private void FindNewTarget()
    {
        Transform closest = null;
        float closestMagn = float.PositiveInfinity;
        foreach (Collider c in CloseEnemies)
        {
            Vector3 dir = c.transform.position - transform.position;
            float angle = Vector3.Dot(dir.normalized, transform.rotation * Vector3.forward);
            if (Mathf.Rad2Deg * Mathf.Acos(angle) <= viewcone)
            {
                Debug.DrawRay(transform.position, dir);
                if(dir.magnitude < closestMagn && c.GetComponent<ABaseAI>().isAlive())
                {
                    closest = c.transform;
                    closestMagn = dir.magnitude;
                }
            }
        }
        if (closest != null)
        {
            target = closest;
            closest.GetComponent<ABaseAI>().Target(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            CloseEnemies.Add(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            CloseEnemies.Remove(other);
            if(other.transform == target)
            {
                target = null;
            }
        }
    }

    private bool IsInView(Transform viewTransform)
    {
        if (CloseEnemies.Contains(viewTransform.GetComponent<Collider>()))
        {
            Vector3 dir = viewTransform.position - transform.position;
            float angle = Vector3.Dot(dir.normalized, transform.rotation * Vector3.forward);
            if (Mathf.Rad2Deg * Mathf.Acos(angle) <= viewcone)
            {
                Debug.DrawRay(transform.position, dir);
                return true;
            }
        }
        return false;
    }
}
