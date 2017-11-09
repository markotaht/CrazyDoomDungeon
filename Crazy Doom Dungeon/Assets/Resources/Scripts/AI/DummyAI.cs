using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyAI : ABaseAI {

    [SerializeField]
    private float defence;

    private Health _health;
    // Use this for initialization
    void Start () {
        _health = GetComponent<Health>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override bool WasHit(float wepStrength)
    {
        float hitStrength = 100 * wepStrength / defence;
        _health.GotHit(hitStrength);
        _health.AddHealth(hitStrength);
        return false;
    }
}
