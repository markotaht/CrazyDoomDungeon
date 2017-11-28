using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollider : MonoBehaviour {

    float damage;
    Collider swordCollider;

	// Use this for initialization
	void Start () {
        damage = transform.parent.GetComponent<Weapon>().getDamage();
        Debug.Log("start: " + damage);
        swordCollider = GetComponent<Collider>();
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(transform.parent.GetComponent<Weapon>().getDamage());
		
	}


    private void OnTriggerEnter(Collider other)
    {
        ABaseAI enemy = other.GetComponent<ABaseAI>();
        if (enemy)
        {
            Debug.Log(damage);
            enemy.WasHit(damage);
        }
    }

    public void EnableCollider(bool enable)
    {
        swordCollider.enabled = enable;
    }
}
