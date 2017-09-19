using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(AttackController))]
[RequireComponent (typeof(EquipmentHandler))]
[RequireComponent (typeof(MovementController))]
public class PlayerController : MonoBehaviour {

    private AttackController attackController;
    private EquipmentHandler equipmentHandler;
    
    private MovementController movementController;

    private UIController uicontroller;
    private float maxHealth = 100.0f;
    private float health;
    private bool alive = true;

    // Use this for initialization
	void Start () {
        attackController = GetComponent<AttackController>();
        equipmentHandler = GetComponent<EquipmentHandler>();
        movementController = GetComponent<MovementController>();
        uicontroller = GameObject.FindObjectOfType<UIController>() as UIController;
        health = maxHealth;
	}

    void Update()
    {

    }

    public void Move(Vector3 target)
    {
        if (alive && attackController.canMove())
        {
            attackController.StopAttacking();
            movementController.Move(target);
        }
    }

    public void Move(Vector2 target)
    {
        if (alive && attackController.canMove())
        {
            attackController.StopAttacking();
            movementController.Move(target);
        }
    }

    public void Attack(Transform target)
    {
        if (alive && target.gameObject.GetComponent<BasicAI>().isAlive())
        {
            attackController.Attack(target, equipmentHandler.getWeapon());
        }
    }

    public void SwapWeapon()
    {
        if (alive)
        {
            equipmentHandler.swapWeapon();
        }
    }

    public Weapon GetEquippedWeapon()
    {
        return equipmentHandler.getWeapon();
    }

    public bool WasHit(float strenght)
    {
        if (alive)
        {
            health -= strenght;
            uicontroller.TakeDamage(strenght);;
            if (health <= 0)
            {
                Die();
            }
        }
        return health <= 0;
    }

    private void Die()
    {
        alive = false;
        equipmentHandler.getWeapon().getAnimator().SetTrigger("die");
        uicontroller.ShowDeathScreen();
        Time.timeScale = 0;
    }

    public void GiveHealth(float hp)
    {
        health = Mathf.Min(health + hp, maxHealth);
        uicontroller.GiveHealth(hp);
    }

}

