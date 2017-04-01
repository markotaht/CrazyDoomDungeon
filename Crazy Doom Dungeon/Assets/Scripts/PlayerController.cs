using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(AttackContoller))]
[RequireComponent (typeof(EquipmentHandler))]
[RequireComponent (typeof(MovementController))]
public class PlayerController : MonoBehaviour {

    private AttackContoller ac;
    private EquipmentHandler eqh;

    //KAs on üldse vaja playerile?
    private MovementController mc;

    // Use this for initialization
	void Start () {
        ac = GetComponent<AttackContoller>();
        eqh = GetComponent<EquipmentHandler>();
        mc = GetComponent<MovementController>();
	}

    public void Move(Vector3 target)
    {
        mc.Move(target);
    }

    public void Attack(Vector3 target)
    {
        ac.Attack(target, eqh.getWeapon());
    }

    public void SwapWeapon()
    {
        eqh.swapWeapon();
    }
}

