using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputHandler : MonoBehaviour {

    public static InputHandler instance;
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
        instance = this;
    }
    

    void Update () {
        if (currentActor == null) return;
        //Makes collision checks work on player character
        if(cc == null)
        {
            cc = currentActor.GetComponent<CharacterController>();
            cc.detectCollisions = true;
        }

        //Joystick movement
        direction = Quaternion.AngleAxis(45, Vector3.up) * jsMovement.InputDirection;
        Transform actor = currentActor.transform;
        //TODO: Magic to make the curve more logaritmic here:
        direction = new Vector3(Mathf.Sin(direction.x * Mathf.PI / 2), Mathf.Sin(direction.y * Mathf.PI / 2), Mathf.Sin(direction.z * Mathf.PI / 2));
        Debug.Log(direction + ", " + jsMovement.InputDirection);
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

    public void Pause(bool pause)
    {
        if (pause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        Debug.Log("Restart Level");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}

