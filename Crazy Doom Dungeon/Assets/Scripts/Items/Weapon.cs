using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : PickupItem{

    [SerializeField]
    private float WeaponRange;
  //  [SerializeField]
    private float WeaponDamage;
    [SerializeField]
    private float WeaponCooldown;
    [SerializeField]
    private Collider WeaponCollider;

    protected Animator animator;

  /*  private void Awake()
    {
        DBitem = ItemLoader.instance.getItem(DatabaseID);
        animator = GetComponent<Animator>();
        Debug.Log(DBitem);
    }*/

    protected override void StartItem()
    {
        WeaponDamage = ((DatabaseWeapon)DBitem).damage;
        animator = GetComponent<Animator>();
    }


    public abstract void StartAttack(Transform target);
    public abstract void DoAttack();

    public override void Use()
    {
        
    }



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

    public float getCooldown()
    {
        return WeaponCooldown;
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
