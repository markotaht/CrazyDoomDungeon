using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour {

    [SerializeField]
    public PlayerController currentActor;

    //private Vector3 target;
    private MoveCommand move = new MoveCommand();
    private AttackCommand attack = new AttackCommand();
    private SwapWeaponCommand swap = new SwapWeaponCommand();

    //private bool pressed = false;
    //private bool moving = false;
    //private GameObject attacking = null;
    
    public float moveSpeed = 1f;
    public VJHandler jsMovement;

    private Vector3 direction;


    public float turningRate = 360f;
    private Quaternion _targetRotation = Quaternion.identity;
    private CharacterController cc;

    // Use this for initialization
    void Start ()
    {
    }
    

    void Update () {

        //Makes collision checks work on player character
        if(cc == null)
        {
            cc = currentActor.GetComponent<CharacterController>();
            cc.detectCollisions = true;
        }

        //Joystick movement
        direction = Quaternion.AngleAxis(45, Vector3.up) * jsMovement.InputDirection;
        Transform actor = currentActor.transform;
        cc.Move(direction * moveSpeed * Time.deltaTime);
        
        //Player rotation
        if (direction != Vector3.zero)
        {
            _targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            actor.rotation = Quaternion.RotateTowards(actor.rotation, _targetRotation, turningRate * Time.deltaTime);
        }


        
        /*Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit,100);
        move.Execute(AGameManager.Instance().MovementDirection(), currentActor);

        target = hit.point;
        //Rewrite to mouse down and up or smthing
        if (Input.GetMouseButton(0))
        {
            if (!pressed)
            {

                //prolly doesn't work on touch screen
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    if (hit.collider)
                    {
                        pressed = true;
                        if (hit.collider.gameObject.tag != "Enemy")
                        {
                            moving = true;
                            move.Execute(target, currentActor);
                        }
                        else
                        {
                            attacking = hit.collider.gameObject;
                            attack.Execute(hit.collider.gameObject.transform, currentActor);
                        }
                    }
                }
            }
            else
            {
                if (hit.collider)
                {
                    if (moving)
                    {
                        move.Execute(target, currentActor);
                    }
                    else
                    {
                        if (!attacking.GetComponent<BasicAI>().isAlive())
                        {
                            if (hit.collider.gameObject.tag == "Enemy")
                            {
                                if (hit.collider.gameObject.GetComponent<BasicAI>().isAlive())
                                {
                                    attacking = hit.collider.gameObject;
                                    attack.Execute(attacking.transform, currentActor);
                                }
                            }
                        }
                        attack.Execute(attacking.transform, currentActor);
                    }
                }
                else if(!moving && attacking.GetComponent<BasicAI>().isAlive())
                {
                    attack.Execute(attacking.transform, currentActor);
                }
            }
        }
        else
        {
            pressed = false;
            moving = false;
        }*/
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwapWeapon();
        }

        if (Input.GetKey("escape"))
        {
            Debug.Log("Quit");
            Application.Quit();
        }
	}

    public void setPlayer(PlayerController controller)
    {
        currentActor = controller;
    }

    public void SwapWeapon()
    {
        swap.Execute(Vector3.up, currentActor);
    }

    public void Attack()
    {
        Debug.Log("ATTACK!");
        currentActor.Attack();
    }
}
