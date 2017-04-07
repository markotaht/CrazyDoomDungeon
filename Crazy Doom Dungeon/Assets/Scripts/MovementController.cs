using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MovementController : MonoBehaviour {

    private Vector3 target;
    private NavMeshAgent agent;

    [SerializeField]
    private float MoveSpeed = 6.0f;

    // Use this for initialization
    void Start () {
        target = transform.position;
        agent = GetComponent<NavMeshAgent>();
        agent.destination = target;
        agent.speed = MoveSpeed;
    }

    public void Move(Vector3 target)
    {
        //  this.target = target;
        agent.destination = target;
        agent.speed = MoveSpeed;
    }

    public void DetachAgent()
    {
        agent.enabled = false;
    }
}
