using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    [SerializeField]
    private Image currentHealth;

    [SerializeField]
    private Text healthText;

    private float maxHealth=100;
    private float health = 100;
    private Image hit;

    private void Start()
    {

        //Debug.Log(GameObject.FindGameObjectWithTag("Hit"));
        hit = GameObject.FindGameObjectWithTag("Hit").GetComponent<Image>();
    }

    private void updateHealthBar()
    {
        float ratio = health / maxHealth;
        currentHealth.rectTransform.localScale = new Vector3(ratio, 1, 1);
        healthText.text = health + "/" + maxHealth;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health < 0)
        {
            health = 0;
        }
        updateHealthBar();
        gotHit();
    }

    public void gotHit()
    {
        StartCoroutine(Flash());
    }

    IEnumerator Flash()
    {
        //Debug.Log(hit);
        hit.enabled = true;
        yield return new WaitForSeconds(0.2f);
        hit.enabled = false;
    }
}
