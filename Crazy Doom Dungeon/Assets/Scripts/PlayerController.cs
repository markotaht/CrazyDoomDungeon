using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(AttackContoller))]
[RequireComponent (typeof(EquipmentHandler))]
[RequireComponent (typeof(MovementController))]
public class PlayerController : MonoBehaviour {

    private AttackContoller attackController;
    private EquipmentHandler equipmentHandler;

    //KAs on üldse vaja playerile?
    private MovementController movementController;

    // Use this for initialization
	void Start () {
        attackController = GetComponent<AttackContoller>();
        equipmentHandler = GetComponent<EquipmentHandler>();
        movementController = GetComponent<MovementController>();
	}

    public void Move(Vector3 target)
    {
        attackController.StopAttacking();
        movementController.Move(target);
    }

    public void Attack(Transform target)
    {
        attackController.Attack(target, equipmentHandler.getWeapon());
    }

    public void SwapWeapon()
    {
        equipmentHandler.swapWeapon();
    }

    public Weapon GetEquippedWeapon()
    {
        return equipmentHandler.getWeapon();
    }
}

