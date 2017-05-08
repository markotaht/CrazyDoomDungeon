using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

[RequireComponent(typeof(MovementController))]
public class BasicAI : MonoBehaviour {

    Vector3 target;
    Transform player;

    [SerializeField]
    private float Detection_distance = 5.0f;
    [SerializeField]
    private float viewcone = 45f;
    [SerializeField]
    private float strength = 2;

    private Animator animator;

    private bool Alive = true;
    
    private float attackCountdown;
    private float attackCooldown;
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
    void Update () {
        RaycastHit hit;
        Vector3 dir = player.position - transform.position;
        LayerMask enemyView = 1 << 8;
        if (Physics.Raycast(transform.position,dir,out hit,Detection_distance,enemyView))
        {
            if(hit.transform == player)
            {
                float angle = Vector3.Dot(dir.normalized, transform.rotation * Vector3.forward);
                if (Mathf.Rad2Deg*Mathf.Acos(angle) <= viewcone)
                {
                    Debug.Log(angle + ", " + Mathf.Rad2Deg * Mathf.Acos(angle));
                    Debug.DrawRay(transform.position, dir);
                    target = player.position;
                }
            }
        }
        if(Vector3.Distance(transform.position,target) > 2 && Alive)
        {
            movementController.Move(target);
        }
        else if(Vector3.Distance(transform.position,target) <= 2 && Alive)
        {
            animator.SetTrigger("attack");
        }
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
