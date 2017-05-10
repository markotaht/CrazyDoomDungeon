using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour {
    private AttackController controller;

    private void Start()
    {
        controller = transform.parent.parent.gameObject.GetComponent<AttackController>();
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            if (controller.isAttacking())
            {
                controller.getWep().getAnimator().SetTrigger("attack");
                other.gameObject.GetComponent<Renderer>().material.color = Color.red;
                other.gameObject.GetComponent<BasicAI>().WasHit(1);
            }
        }
    }
}
