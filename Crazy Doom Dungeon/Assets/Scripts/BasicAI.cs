using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementController))]
public class BasicAI : MonoBehaviour {

    Vector3 target;
    Transform player;

    [SerializeField]
    private float Detection_distance = 5.0f;
    [SerializeField]
    private float viewcone = 45f;

    private bool Alive = true;

    private MovementController mc;
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        mc = GetComponent<MovementController>();
        target = transform.position;
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
                float angle = Vector3.Dot(dir, transform.rotation * Vector3.left);
                if (Mathf.Acos(angle) <= viewcone)
                {
                    target = player.position;
                }
            }
        }

        if(Vector3.Distance(transform.position,target) > 2 && Alive)
        {
            mc.Move(target);
        }
	}

    public void Die()
    {
        Alive = false;
    }
}
