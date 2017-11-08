using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon {
    
    [SerializeField]
    private GameObject ammoType;
    [SerializeField]
    private Transform positionOffset;
    
    // Use this for initialization
    void Start () {

	}

    public override void StartAttack(Transform target)
    {

        animator.SetTrigger("attack");
    }

    public override void DoAttack(Transform target)
    {
        //MA EI OSKA! if target == null -> target.pos = pos in front of player/weapon
        Debug.Log(target);
        Vector3 targetPos = (target != null ? target.position : (transform.position + 10*transform.forward));
        Vector3 direction = targetPos - transform.position;
        GameObject ammo = Instantiate(ammoType);
        // direction += ammo.transform.rotation.eulerAngles;
        //ammo.transform.position = transform.position + transform.rotation * Vector3.forward * 2;
        ammo.transform.position = positionOffset.position;// + transform.rotation * Vector3.forward * 0.5f; //for arrow
        ammo.transform.rotation = Quaternion.LookRotation(direction);
        //Projectile pro = ammo.GetComponent<Projectile>();
        StraightProjectile pro = ammo.GetComponent<StraightProjectile>();
        pro.attack(targetPos);
    }
}
