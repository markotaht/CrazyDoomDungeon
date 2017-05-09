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

    // Use this for initialization
	void Start () {
        attackController = GetComponent<AttackController>();
        equipmentHandler = GetComponent<EquipmentHandler>();
        movementController = GetComponent<MovementController>();
        uicontroller = GameObject.FindObjectOfType<UIController>() as UIController;
	}

    void Update()
    {
        Debug.Log(health);
    }

    public void Move(Vector3 target)
    {
        if (attackController.canMove())
        {
            attackController.StopAttacking();
            movementController.Move(target);
        }
    }

    public void Attack(Transform target)
    {
        if (target.gameObject.GetComponent<BasicAI>().isAlive())
        {
            attackController.Attack(target, equipmentHandler.getWeapon());
        }
    }

    public void SwapWeapon()
    {
        equipmentHandler.swapWeapon();
    }

    public Weapon GetEquippedWeapon()
    {
        return equipmentHandler.getWeapon();
    }

    public bool WasHit(float strenght)
    {
        health -= strenght;
        uicontroller.gotHit();
        uicontroller.updateHealth(health);
        return health <= 0;
    }
}

