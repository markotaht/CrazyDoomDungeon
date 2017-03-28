using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentHandler : MonoBehaviour {

    [SerializeField]
    private Weapon EquipedWeapon;

    [SerializeField]
    private Weapon SecondaryWeapon;

	// Use this for initialization
	void Start () {
        SecondaryWeapon.gameObject.SetActive(false);
	}
	
    public Weapon getWeapon()
    {
        return EquipedWeapon;
    }

    public void swapWeapon()
    {
        Weapon temp = EquipedWeapon;
        EquipedWeapon = SecondaryWeapon;
        SecondaryWeapon = temp;
        SecondaryWeapon.gameObject.SetActive(false);
        EquipedWeapon.gameObject.SetActive(true);
    }
}
