using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollider : MonoBehaviour {

    float damage;
    Collider swordCollider;

	// Use this for initialization
	void Start () {
        damage = transform.parent.GetComponent<Weapon>().getDamage();
        swordCollider = GetComponent<Collider>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnTriggerEnter(Collider other)
    {
        ABaseAI enemy = other.GetComponent<ABaseAI>();
        if (enemy)
        {
            enemy.WasHit(damage);
        }
    }

    public void EnableCollider(bool enable)
    {
        swordCollider.enabled = enable;
    }
}
