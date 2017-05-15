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
    private GameObject[] deathScreen;
    private Text loadingText;
    private Text mobCounter;
    private Text timeCounter;

    private void Start()
    {
        hit = GameObject.FindGameObjectWithTag("Hit").GetComponent<Image>();
        deathScreen = GameObject.FindGameObjectsWithTag("DeathScreen");
        loadingText = GameObject.FindGameObjectWithTag("Loading").GetComponent<Text>();
        mobCounter = GameObject.FindGameObjectWithTag("EnemyCounter").GetComponent<Text>();
        timeCounter = GameObject.FindGameObjectWithTag("TimeCounter").GetComponent<Text>();
    }

    public void UpdateHealthBar()
    {
        float ratio = health / maxHealth;
        currentHealth.rectTransform.localScale = new Vector3(ratio, 1, 1);
        healthText.text = health + "/" + maxHealth;
    }

    public void UpdateMobCounter(int count)
    {
        mobCounter.text = "Bears: " + count;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health < 0)
        {
            health = 0;
        }
        UpdateHealthBar();
        GotHit();
    }

    public void GiveHealth(float hp)
    {
        health = Mathf.Min(health + hp, maxHealth);
        UpdateHealthBar();
    }

    public void ShowDeathScreen()
    {
        foreach(GameObject ds in deathScreen)
        {
            ds.GetComponent<Text>().enabled = true;
        }
    }

    public void ShowLoading(bool show)
    {
        loadingText.enabled = show;
    }

    public void GotHit()
    {
        StartCoroutine(Flash());
    }

    IEnumerator Flash()
    {
        hit.enabled = true;
        yield return new WaitForSeconds(0.2f);
        hit.enabled = false;
    }
}
