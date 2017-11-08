using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : Item{

    [SerializeField]
    private float WeaponRange;
    [SerializeField]
    private float WeaponDamage;
    [SerializeField]
    private float WeaponWindupSpeed;
    [SerializeField]
    private float WeaponWinddownSpeed;
    [SerializeField]
    private Collider WeaponCollider;

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

    public void EnableCollider()
    {
        if (WeaponCollider != null)
        {
            WeaponCollider.enabled = true;
        }
    }

    public void DisableCollider()
    {
        if (WeaponCollider != null)
        {
            WeaponCollider.enabled = false;
        }
    }
}
