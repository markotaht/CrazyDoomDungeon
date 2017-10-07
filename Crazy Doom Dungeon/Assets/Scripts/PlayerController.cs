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


    private Rigidbody rb;

    // Use this for initialization
    void Start () {
        attackController = GetComponent<AttackController>();
        equipmentHandler = GetComponent<EquipmentHandler>();
        movementController = GetComponent<MovementController>();
        uicontroller = GameObject.FindObjectOfType<UIController>() as UIController;
        health = maxHealth;
        //rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //Debug.Log(transform.position.y);
        if(transform.position.y != 0)
        {
            transform.position = new Vector3(transform.position.x, 1.1f, transform.position.z);
        }
        //rb.velocity = Vector3.zero;
        //rb.angularVelocity = Vector3.zero;
    }

    public void Move(Vector3 target)
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
    /*
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(rb.angularVelocity);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        
    }*/
}

