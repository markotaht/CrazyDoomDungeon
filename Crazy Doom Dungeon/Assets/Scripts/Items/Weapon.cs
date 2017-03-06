using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour{

    [SerializeField]
    private float WeaponRange = 1.0f;
    [SerializeField]
    private float WeaponDamage = 1.0f;

    public float getRange()
    {
        return WeaponRange;
    }

    public float getDamage()
    {
        return WeaponDamage;
    }

    public abstract void Attack(Vector3 direction);
}
