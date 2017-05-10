using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

[RequireComponent(typeof(MovementController))]
public class BasicAI : MonoBehaviour {

    Vector3 target;
    bool targetSelf = true;
    Transform player;

    private float health = 100;

    enum State { READY_TO_ATTACK, ATTACK_WINDUP, ATTACK_WINDDOWN };
    private State current_state = State.READY_TO_ATTACK;

    [SerializeField]
    private float detectionDistance;
    [SerializeField]
    private float viewcone;
    [SerializeField]
    private float strength;
    [SerializeField]
    private float defence; //cannot be 0, <1 make attacks worse
    [SerializeField]
    private float range;

    private Animator animator;

    private bool Alive = true;
    private bool sawPlayer = false;
    private float fromSeeingPlayer = 0;

    [SerializeField]
    private float attackWinddown;
    [SerializeField]
    private float attackWindup;

    private float attackCountdown;

    private bool inAttackAnim = false;

    private MovementController movementController;
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        movementController = GetComponent<MovementController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update ()
    {
        if (!Alive)
        {
            return;
        }
        attackCountdown -= Time.deltaTime;
        if (CanSeePlayer() || current_state == State.ATTACK_WINDUP)
        {
            sawPlayer = true;
            fromSeeingPlayer = 0;
            float dist;
            switch (current_state)
            {
                case State.READY_TO_ATTACK:
                    dist = Vector3.Distance(transform.position, target);
                    if (dist > range)
                    {
                        movementController.Move(transform.position + (target - transform.position).normalized * (dist - range + 1));
                    }
                    else
                    {
                        //Stop and turn to player
                        movementController.Move(transform.position);
                        Vector3 planarTarget = new Vector3(target.x, 0, target.z);
                        Vector3 planarPosition = new Vector3(transform.position.x, 0, transform.position.z);
                        Vector3 direction = planarTarget - planarPosition;
                        transform.rotation = Quaternion.LookRotation(direction.normalized);

                        animator.SetTrigger("attack");
                        attackCountdown = attackWindup;
                        current_state = State.ATTACK_WINDUP;
                    }
                    return;

                case State.ATTACK_WINDUP:
                    dist = Vector3.Distance(transform.position, target);
                    if (dist > range && attackCountdown >= 0.7*attackWindup)
                    {
                        movementController.Move(transform.position + (target - transform.position).normalized * (dist - range + 1));
                    }
                    if (attackCountdown <= 0)
                    {
                        bool killed = player.GetComponent<PlayerController>().WasHit(strength);
                        attackCountdown = attackWinddown;
                        current_state = State.ATTACK_WINDDOWN;
                    }
                    return;

                case State.ATTACK_WINDDOWN:
                    if (attackCountdown <= 0)
                    {
                        current_state = State.READY_TO_ATTACK;
                    }
                    return;
            }
        }
        else if (sawPlayer && fromSeeingPlayer < 3)
        {
            fromSeeingPlayer += Time.deltaTime;
            transform.rotation *= Quaternion.Euler(0, Time.deltaTime*Mathf.Rad2Deg*2, 0);
        }
	}

    private bool CanSeePlayer()
    {
        RaycastHit hit;
        Vector3 dir = player.position - transform.position;
        LayerMask enemyView = 1 << 8;
        if (Physics.Raycast(transform.position, dir, out hit, detectionDistance, enemyView))
        {
            if (hit.transform == player)
            {
                float angle = Vector3.Dot(dir.normalized, transform.rotation * Vector3.forward);
                if (Mathf.Rad2Deg * Mathf.Acos(angle) <= viewcone)
                {
                    Debug.DrawRay(transform.position, dir);
                    target = player.position;
                    targetSelf = false;
                    return true;
                }
            }
        }
        return false;
    }

    public void Die()
    {
        animator.SetTrigger("die");
        Alive = false;
        movementController.DetachAgent();
        transform.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        //TODO: remove
        if (GetComponent<Renderer>())
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
    }

   public bool isAlive()
    {
        return Alive;
    }

    public bool WasHit(float wepStrength)
    {
        float hitStrength = 100 * wepStrength / defence;
        health -= hitStrength;
        Debug.Log(health);
        if (health <= 0)
        {
            Die();
            return true;
        }
        return false;
    }
}
