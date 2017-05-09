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

    enum State { READY_TO_ATTACK, ATTACK_WINDUP, ATTACK_WINDDOWN };
    private State current_state = State.READY_TO_ATTACK;

    [SerializeField]
    private float detectionDistance;
    [SerializeField]
    private float viewcone;
    [SerializeField]
    private float strength;
    [SerializeField]
    private float range;

    private Animator animator;

    private bool Alive = true;

    [SerializeField]
    private float attackWinddown;
    [SerializeField]
    private float attackWindup;

    private float attackCountdown;
    //private float attackCooldown;
    private bool inAttackAnim = false;

    private MovementController movementController;
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        movementController = GetComponent<MovementController>();
        target = transform.position;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update ()
    {
        attackCountdown -= Time.deltaTime;
        Debug.Log(current_state);
        if (CanSeePlayer() && Alive)
        {
            //Debug.Log("CAN see");
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
                    break;

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
                    break;

                case State.ATTACK_WINDDOWN:
                    if (attackCountdown <= 0)
                    {
                        //target = null;
                        current_state = State.READY_TO_ATTACK;
                    }
                    break;
            }
            /*
            float dist = Vector3.Distance(transform.position, target);
            if (dist <= range)
            {
                Debug.Log("ATTACK");
                //TODO
            }
            else
            {
                movementController.Move(transform.position + (target - transform.position).normalized * (dist - range + 1));
            }*/
        }
        else
        {
            //Debug.Log("can't see");
        }
        /*if(Vector3.Distance(transform.position,target) > range && Alive && !targetSelf)
        {
            movementController.Move(target);
        }
        else if (Vector3.Distance(transform.position,target) <= range && Alive && !targetSelf)
        {
            Debug.Log(Vector3.Distance(transform.position, target) + ", " + transform.position + " and "  + target);
            animator.SetTrigger("attack");
        }*/
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

    public bool WasHit()
    {
        Die();
        return true;
    }


}
