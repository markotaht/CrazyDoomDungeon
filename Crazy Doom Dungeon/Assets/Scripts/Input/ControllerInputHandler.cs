using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControllerInputHandler : AInputHandler
{

    [SerializeField]
    private GameObject menuPanel;

    [SerializeField]
    private Button inventoryButton;

    [SerializeField]
    private Button potionButton;

    [SerializeField]
    private Slot firstSlot;

    [SerializeField]
    private Button swap;

    private bool waszero = false;

    // Use this for initialization
    void Start()
    {
        instance = this;
    }


    void Update()
    {
        if (currentActor == null) return;
        //Makes collision checks work on player character
        if (cc == null)
        {
            cc = currentActor.GetComponent<CharacterController>();
            cc.detectCollisions = true;
        }

        //Joystick movement
        float left = Input.GetAxis("Horizontal");
        float up = Input.GetAxis("Vertical");

        direction = Quaternion.AngleAxis(45, Vector3.up) * new Vector3(left,0,up );
        //      direction = Quaternion.AngleAxis(45, Vector3.up) * jsMovement.InputDirection;
        Debug.Log(left + ", " + up);
        if(left != 0 || up != 0)
        {
            //sets visual joystick to controller position
            jsMovement.SetJoystickPosition(left, up);
            waszero = false;
        }
        else if(!waszero && left == 0 && up == 0)
        {
            jsMovement.SetJoystickPosition(left, up);
            waszero = true;
        }
        Transform actor = currentActor.transform;
        cc.Move(direction * moveSpeed * Time.deltaTime);

        //Player rotation
        if (direction != Vector3.zero)
        {
            _targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            actor.rotation = Quaternion.RotateTowards(actor.rotation, _targetRotation, turningRate * Time.deltaTime);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }

        if (Input.GetButtonDown("Swap"))
        {
            if (swap.interactable)
            {
                swap.onClick.Invoke();
            }
         //   SwapWeapon();
        }

        if (Input.GetButtonDown("Menu"))
        {
            Pause(true);
            menuPanel.SetActive(true);
            menuPanel.transform.GetChild(1).GetComponent<Button>().Select();
        }

        if (Input.GetButtonDown("Potion"))
        {
            potionButton.onClick.Invoke();
         /*   if (inventoryButton.interactable)
            {
                inventoryButton.onClick.Invoke();
                firstSlot.Select();
            }*/
        }

        if (Input.GetKey("escape"))
        {
            Debug.Log("Quit");
            Application.Quit();
        }
    }

    
}

