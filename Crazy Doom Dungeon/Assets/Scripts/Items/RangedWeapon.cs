using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon {

    //TODO projectile
    
    // Use this for initialization
    void Start () {
		
	}

    public override void Attack(Vector3 direction)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = transform.position;
        cube.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        Projectile pro = cube.AddComponent(typeof(Projectile)) as Projectile;
        pro.setDirection(direction);
    }
}
