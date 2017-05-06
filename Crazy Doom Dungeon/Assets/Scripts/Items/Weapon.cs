using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : Item{

    [SerializeField]
    private float WeaponRange = 1.0f;
    [SerializeField]
    private float WeaponDamage = 1.0f; //unused
    [SerializeField]
    private float WeaponWindupSpeed = 0.5f;
    [SerializeField]
    private float WeaponWinddownSpeed = 0.5f;

    protected Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    public abstract void StartAttack(Transform target);
    public abstract void DoAttack(Transform target);



    public float getRange()
    {
        return WeaponRange;
    }

    public float getDamage()
    {
        return WeaponDamage;
    }

    public Animator getAnimator()
    {
        return animator;
    }

    public float getWindupSpeed()
    {
        return WeaponWindupSpeed;
    }

    public float getWinddownSpeed()
    {
        return WeaponWinddownSpeed;
    }

    
}
