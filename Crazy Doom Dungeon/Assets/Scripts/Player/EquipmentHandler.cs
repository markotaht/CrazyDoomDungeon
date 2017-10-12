using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentHandler : MonoBehaviour {
    /*
    [SerializeField]
    private Weapon[] weapons;
    private Weapon EquippedWeapon;

    [SerializeField]
    private Weapon SecondaryWeapon;
    
	// Use this for initialization
	void Start () {
        SecondaryWeapon.gameObject.SetActive(false);
        EquippedWeapon.gameObject.SetActive(true);
	}
	
    public Weapon getWeapon()
    {
        return EquippedWeapon;
    }

    public void swapWeapon()
    {
        Weapon temp = EquippedWeapon;
        EquippedWeapon = SecondaryWeapon;
        SecondaryWeapon = temp;
        SecondaryWeapon.gameObject.SetActive(false);
        EquippedWeapon.gameObject.SetActive(true);
    }*/

    [SerializeField]
    private Weapon EquippedWeapon;

    [SerializeField]
    private Weapon SecondaryWeapon;

    private EquipmentGUI GUI;
    
	// Use this for initialization
	void Start () {
        GUI = GetComponent<EquipmentGUI>();
        if(SecondaryWeapon != null)
            SecondaryWeapon.gameObject.SetActive(false);
        if(EquippedWeapon != null)
            EquippedWeapon.gameObject.SetActive(true);
	}
	
    public Weapon getWeapon()
    {
        return EquippedWeapon;
    }

    public void swapWeapon()
    {
        Weapon temp = EquippedWeapon;
        EquippedWeapon = SecondaryWeapon;
        SecondaryWeapon = temp;
        SecondaryWeapon.gameObject.SetActive(false);
        EquippedWeapon.gameObject.SetActive(true);
    }
}
