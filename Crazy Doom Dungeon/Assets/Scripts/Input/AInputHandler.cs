using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public abstract class AInputHandler : MonoBehaviour {

    public static AInputHandler instance;

    [SerializeField]
    protected PlayerController currentActor;

    protected MoveCommand move = new MoveCommand();
    protected AttackCommand attack = new AttackCommand();
    protected SwapWeaponCommand swap = new SwapWeaponCommand();

    public float moveSpeed = 1f;
    public VJHandler jsMovement;

    protected Vector3 direction;

    public float turningRate = 360f;
    protected Quaternion _targetRotation = Quaternion.identity;
    protected CharacterController cc;

	// Update is called once per frame
	void Update () {
		
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

    public void UsePotion()
    {

    }
}
