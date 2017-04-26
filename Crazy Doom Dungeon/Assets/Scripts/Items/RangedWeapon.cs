using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon {

    //TODO projectile
    [SerializeField]
    private float FireRate = 0.0f;

    [SerializeField]
    private float MaxRange = 10.0f;

    [SerializeField]
    private GameObject ammoType;

    private float NextFire = 0.0f;
    
    // Use this for initialization
    void Start () {
		
	}

    public override void Attack(Transform target)
    {

        animator.SetTrigger("attack");
        /*
        if (Time.time > NextFire)
        {
            NextFire = Time.time + FireRate;
            GameObject ammo = Instantiate(ammoType);
            ammo.transform.position = transform.position + transform.rotation * Vector3.forward * 2;
            ammo.transform.rotation = Quaternion.LookRotation(direction);
            Projectile pro = ammo.GetComponent<Projectile>();
            pro.setDirection(direction.normalized * (direction.magnitude % MaxRange));
        }*/
    }
}
