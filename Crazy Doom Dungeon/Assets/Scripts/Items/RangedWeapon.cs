using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon {
    
    [SerializeField]
    private GameObject ammoType;
    
    // Use this for initialization
    void Start () {
		
	}

    public override void StartAttack(Transform target)
    {

        animator.SetTrigger("attack");
    }

    public override void DoAttack(Transform target)
    {
        Vector3 direction = target.position - transform.position;
        GameObject ammo = Instantiate(ammoType);
        ammo.transform.position = transform.position + transform.rotation * Vector3.forward * 2;
        ammo.transform.rotation = Quaternion.LookRotation(direction);
        Projectile pro = ammo.GetComponent<Projectile>();
        pro.attack(target);
    }
}
