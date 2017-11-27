using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon {

    // Update is called once per frame
    void Update () {
		
	}

    public override void StartAttack(Transform target)
    {
        animator.SetTrigger("attack");
        //GetComponent<MeshCollider>().enabled = true;
    }

    public override void DoAttack()
    {
        //bool killed = target.GetComponent<BasicAI>().WasHit(getDamage());
    }
    
}
