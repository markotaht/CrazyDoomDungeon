using System.Collections;
using System.Collections.Generic;
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
    private float xMin, xMax, yMin, yMax;


    public float turningRate = 360f;
    private Quaternion _targetRotation = Quaternion.identity;
    private CharacterController cc;

    // Use this for initialization
    void Start () {
        //Initialization of boundaries
        xMax = 500; // I used 50 because the size of player is 100*100
        xMin = -500;
        yMax = 500;
        yMin = -500;
    }
    

    // Update is called once per frame
    void Update () {

        cc = currentActor.GetComponent<CharacterController>();
        cc.detectCollisions = true;
        //Joystick movement
        direction = Quaternion.AngleAxis(45, Vector3.up)*  jsMovement.InputDirection; //InputDirection can be used as per the need of your project
        Transform actor = currentActor.transform;
        //currentActor.transform.position += direction * moveSpeed * Time.deltaTime;
        cc.Move(direction * moveSpeed * Time.deltaTime);
        
        if (direction != Vector3.zero)
        {
            _targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            //actor.rotation = Quaternion.LookRotation(direction, Vector3.up);
            actor.rotation = Quaternion.RotateTowards(actor.rotation, _targetRotation, turningRate * Time.deltaTime);
        }
        

        /*
        if (direction.magnitude != 0)
        {

            Transform actor = currentActor.transform;
            float rotSpeed = 3000;
            //target += direction * moveSpeed;
            //Vector3 target = currentActor.gameObject.transform.position + (direction)*2;
            currentActor.transform.position += direction * moveSpeed * Time.deltaTime;

            //Calculate angle we need to rotate
            float angle = Vector3.Angle(actor.forward, direction);

            //Do we rotate to the left or right
            if (Vector3.Cross(actor.forward, direction).y < 0)
            {
                rotSpeed *= -1;
            }

            //If we already are at the rotaton dont rotate
            if (angle > 1)
            {
                //Calculate the next rotation we want
                Quaternion desired = Quaternion.Euler(actor.eulerAngles.x, actor.eulerAngles.y + rotSpeed * Time.deltaTime, actor.eulerAngles.z);

                //If we are near the desired rotation, set rotation to desired rotation
                if (angle <= 1)
                {
                    actor.rotation = desired;
                }

                //If we have not finished rotation then rotate
                else if (!actor.rotation.Equals(desired))
                {
                    Debug.Log("here");
                    actor.rotation = Quaternion.Slerp(actor.rotation, desired, Time.deltaTime * 10);
                }
            }
          //  currentActor.transform.rotation *= Quaternion.FromToRotation(currentActor.transform.rotation * Vector3.forward, direction);
            //currentActor.transform.position = new Vector3(Mathf.Clamp(currentActor.transform.position.x, xMin, xMax), 0f, Mathf.Clamp(currentActor.transform.position.y, yMin, yMax));//to restric movement of player
            //move.Execute(target, currentActor);
        }*/

        /*Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit,100);

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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            move.Execute(currentActor.transform.position, currentActor);
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
}
