using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour {

    [SerializeField]
    public PlayerController currentActor;

    private Vector3 target;
    private MoveCommand move = new MoveCommand();
    private AttackCommand attack = new AttackCommand();
    private SwapWeaponCommand swap = new SwapWeaponCommand();

    private bool pressed = false;
    private bool moving = false;
    private GameObject attacking = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit,100);

        target = hit.point;
        if (Input.GetMouseButton(0))
        {
            if (!pressed)
            {
                //Debug.Log("Pressed button");
                pressed = true;
                //hit.collider == null || ??
                if (hit.collider.gameObject.tag != "Enemy")
                {
                    //Debug.Log("Will start moving");
                    moving = true;
                    move.Execute(target, currentActor);
                }
                else
                {
                    attacking = hit.collider.gameObject;
                    //Debug.Log("Will start attacking " + attacking);
                    attack.Execute(hit.collider.gameObject.transform, currentActor);
                }

            }
            else
            {
                if (moving)
                {
                    move.Execute(target, currentActor);
                }
                else
                {
                    if (!attacking.GetComponent<BasicAI>().isAlive())
                    {
                        //Debug.Log(attacking + " died");
                        if (hit.collider.gameObject.tag == "Enemy")
                        {
                            attacking = hit.collider.gameObject;
                            //Debug.Log("Started attacking " + attacking);
                            attack.Execute(attacking.transform, currentActor);
                        }
                    }
                    else
                    {
                        attack.Execute(attacking.transform, currentActor);
                    }
                }
            }
        }
        else
        {
            pressed = false;
            moving = false;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            swap.Execute(Vector3.up, currentActor);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            move.Execute(currentActor.transform.position, currentActor);
        }
	}

    public void setPlayer(PlayerController controller)
    {
        currentActor = controller;
    }
}
