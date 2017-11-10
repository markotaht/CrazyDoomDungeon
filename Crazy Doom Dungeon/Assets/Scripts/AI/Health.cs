using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Health : MonoBehaviour {

    [SerializeField]
    private Image HPBar;

    [SerializeField]
    private GameObject DamagePrefab;

    [SerializeField]
    private Transform DamageTransform;

    [SerializeField]
    private float MaxHealth;

    private float _CurrentHealth;
    public float CurrentHealth
    {
        get
        {
            return _CurrentHealth;
        }
    }
	// Use this for initialization
	void Start () {
        _CurrentHealth = MaxHealth;
	}

    public void GotHit(float hit)
    {
        _CurrentHealth -= hit;
        HPBar.fillAmount = CurrentHealth / MaxHealth;
        GameObject damage = Instantiate(DamagePrefab,DamageTransform.position, DamageTransform.rotation, DamageTransform);
        damage.GetComponentInChildren<Text>().text = hit.ToString();
    }

    public void AddHealth(float heal)
    {
        _CurrentHealth += heal;
        HPBar.fillAmount = CurrentHealth / MaxHealth;
    }
    

}
