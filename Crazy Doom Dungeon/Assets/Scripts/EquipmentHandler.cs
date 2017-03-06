using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentHandler : MonoBehaviour {

    [SerializeField]
    private Weapon EquipedWeapon;

	// Use this for initialization
	void Start () {
		
	}
	
    public Weapon getWeapon()
    {
        return EquipedWeapon;
    }
}
