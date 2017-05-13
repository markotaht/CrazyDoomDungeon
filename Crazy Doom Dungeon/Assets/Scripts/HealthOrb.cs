using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthOrb : MonoBehaviour {

    private PlayerController pc;
    [SerializeField]
    private float healthValue;

	// Use this for initialization
	void Start ()
    {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            pc.GiveHealth(healthValue);
            Destroy(gameObject);
        }
    }
}
