using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : Item{

    [SerializeField]
    private float WeaponRange = 1.0f;
    [SerializeField]
    private float WeaponDamage = 1.0f;
    [SerializeField]
    private float WeaponWindupSpeed = 0.5f;
    [SerializeField]
    private float WeaponAttackSpeed = 1.0f;

    protected Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public float getRange()
    {
        return WeaponRange;
    }

    public float getDamage()
    {
        return WeaponDamage;
    }

    public abstract void Attack(Transform target);

    public Animator getAnimator()
    {
        return animator;
    }

    public float getWindupSpeed()
    {
        return WeaponWindupSpeed;
    }

    public float getAttackSpeed()
    {
        return WeaponAttackSpeed;
    }
}
