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
    public Weapon EquippedWeapon;

    [SerializeField]
    public Weapon SecondaryWeapon;

    private EquipmentGUI GUI;
    
	// Use this for initialization
	void Start () {
        GUI = EquipmentGUI.instance;
        if (SecondaryWeapon != null)
        {
            GUI.AddSecondary(SecondaryWeapon.gameObject);
            SecondaryWeapon.gameObject.SetActive(false);
        }
        if (EquippedWeapon != null)
        {
            GUI.AddPrimary(EquippedWeapon.gameObject);
            EquippedWeapon.gameObject.SetActive(true);
        }
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
        if(SecondaryWeapon != null)
        SecondaryWeapon.gameObject.SetActive(false);
        if(EquippedWeapon != null)
        EquippedWeapon.gameObject.SetActive(true);
    }

    public void SetSecondary(Weapon wep)
    {
        if (wep != null)
        {
            wep.transform.parent = transform;
            wep.gameObject.SetActive(false);
        }else
        {
            Destroy(SecondaryWeapon.gameObject);
        }
        SecondaryWeapon = wep;
    }

    public void SetPrimary(Weapon wep)
    {
        if (wep != null)
        {
            wep.transform.parent = transform;
            wep.gameObject.SetActive(true);
        }else
        {
            Destroy(EquippedWeapon.gameObject);
        }
        EquippedWeapon = wep;
    }
}
