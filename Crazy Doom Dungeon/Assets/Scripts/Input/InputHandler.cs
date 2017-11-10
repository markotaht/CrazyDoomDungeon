using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour {

    [SerializeField]
    public PlayerController currentActor;
    
    private MoveCommand move = new MoveCommand();
    private AttackCommand attack = new AttackCommand();
    private SwapWeaponCommand swap = new SwapWeaponCommand();
    
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
        currentActor.Attack();
    }
}
