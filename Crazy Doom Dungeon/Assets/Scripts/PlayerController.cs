using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(AttackController))]
[RequireComponent (typeof(EquipmentHandler))]
[RequireComponent (typeof(MovementController))]
public class PlayerController : MonoBehaviour {

    private AttackController attackController;
    private EquipmentHandler equipmentHandler;
    
    private MovementController movementController;

    private UIController uicontroller;
    private float health = 100.0f;
    private bool alive = true;

    // Use this for initialization
	void Start () {
        attackController = GetComponent<AttackController>();
        equipmentHandler = GetComponent<EquipmentHandler>();
        movementController = GetComponent<MovementController>();
        uicontroller = GameObject.FindObjectOfType<UIController>() as UIController;
	}

    void Update()
    {
        if(!alive)
        {
            //TODO: show death
            //Debug.Log("YOU ARE DEAD");
        }
    }

    public void Move(Vector3 target)
    {
        if (alive && attackController.canMove())
        {
            attackController.StopAttacking();
            movementController.Move(target);
        }
    }

    public void Attack(Transform target)
    {
        if (alive && target.gameObject.GetComponent<BasicAI>().isAlive())
        {
            attackController.Attack(target, equipmentHandler.getWeapon());
        }
    }

    public void SwapWeapon()
    {
        if (alive)
        {
            equipmentHandler.swapWeapon();
        }
    }

    public Weapon GetEquippedWeapon()
    {
        return equipmentHandler.getWeapon();
    }

    public bool WasHit(float strenght)
    {
        if (alive)
        {
            health -= strenght;
            uicontroller.gotHit();
            uicontroller.updateHealth(health);
            if (health <= 0)
            {
                Die();
            }
        }
        return health <= 0;
    }

    private void Die()
    {
        Debug.Log("YOU ARE DEAD");
        alive = false;
    }
}

