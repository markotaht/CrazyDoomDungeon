using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon {

    //TODO projectile
    [SerializeField]
    private float FireRate = 1.0f;
    private float NextFire = 0.0f;
    
    // Use this for initialization
    void Start () {
		
	}

    public override void Attack(Vector3 direction)
    {
        if (Time.time > NextFire)
        {
            NextFire = Time.time + FireRate;
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = transform.position + transform.rotation * Vector3.forward  * 2;
            cube.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            Projectile pro = cube.AddComponent(typeof(Projectile)) as Projectile;
            pro.setDirection(direction);
        }
    }
}
