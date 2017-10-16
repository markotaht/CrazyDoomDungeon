using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//using System;

[RequireComponent(typeof(MovementController))]
public class BasicAI : MonoBehaviour {

    Vector3 target;
    bool targetSelf = true;
    Transform player;

    private float maxHealth = 100;
    private float health;

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
    private bool playerKilled = false;

    [SerializeField]
    private float attackWinddown;
    [SerializeField]
    private float attackWindup;

    private float attackCountdown;

    private int turningDirection;

    private bool inAttackAnim = false;

    private Texture2D healthEmpty;
    private Texture2D healthFull;

    private UIController uicontroller;
    private MovementController movementController;
    // Use this for initialization
    void Start () {
        health = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        movementController = GetComponent<MovementController>();
        animator = GetComponent<Animator>();
        healthEmpty = Resources.Load("HealthbarEmptyColor") as Texture2D;
        healthFull = Resources.Load("HealthbarColor") as Texture2D;
        turningDirection = (Random.value > 0.5f) ? 1 : -1;

        uicontroller = GameObject.FindGameObjectWithTag("UI").GetComponent<UIController>();
        uicontroller.UpdateMobCounter(1);
    }

    private void OnGUI()
    {
        Vector2 target_pos;
        target_pos = Camera.main.WorldToScreenPoint(transform.position);
        GUI.depth = 100;
        GUI.BeginGroup(new Rect(target_pos.x - 30, Screen.height - target_pos.y - 50, 60, 5));
            GUI.DrawTexture(new Rect(0, 0, 60, 5), healthEmpty);
            GUI.BeginGroup(new Rect(0, 0, 60 * health/maxHealth, 5));
                GUI.DrawTexture(new Rect(0, 0, 60, 5), healthFull);
            GUI.EndGroup();
        GUI.EndGroup();
    }

    // Update is called once per frame
    void Update ()
    {
        if (!Alive || playerKilled)
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
                        playerKilled = player.GetComponent<PlayerController>().WasHit(strength);
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
            transform.rotation *= Quaternion.Euler(0, turningDirection*Time.deltaTime*Mathf.Rad2Deg*2, 0);
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
                    //Debug.DrawRay(transform.position, dir);
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
        if (!Alive) return;
        animator.SetTrigger("die");
        Alive = false;
        movementController.DetachAgent();
        GetComponent<BoxCollider>().enabled = false;
        transform.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        transform.SendMessage("DropSomething");
        uicontroller.UpdateMobCounter(-1);
    }

   public bool isAlive()
    {
        return Alive;
    }

    public bool WasHit(float wepStrength)
    {
        float hitStrength = 100 * wepStrength / defence;
        health -= hitStrength;
        if (health <= 0)
        {
            Die();
            return true;
        }
        if (!sawPlayer || sawPlayer && fromSeeingPlayer > 3)
        {
            sawPlayer = true;
            fromSeeingPlayer = 0;
        }
        return false;
    }
}
