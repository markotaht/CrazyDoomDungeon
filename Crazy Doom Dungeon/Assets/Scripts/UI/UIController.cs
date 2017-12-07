using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

    [SerializeField]
    private Image currentHealth;

    [SerializeField]
    private Text healthText;

    private float maxHealth=100;
    private float health = 100;
    private Image hit;
    private GameObject[] deathScreen;
    private GameObject[] winScreen;
    //private Text loadingText;
    
    //private GameObject UIButtons;
    //public Button startButton;
    //public Button quitButton;
    public GameObject controlPanel;
    public GameObject menuPanel;
    public Button resumeButton;
    public Text pauseText;
    public GameObject winPanel;

    private int mobCount = 0;
    private Text mobCounter;
    private Text timeCounter;
    private bool countingTime = true;

    private void Start()
    {
        hit = GameObject.FindGameObjectWithTag("Hit").GetComponent<Image>();
        deathScreen = GameObject.FindGameObjectsWithTag("DeathScreen");
        winScreen = GameObject.FindGameObjectsWithTag("WinScreen");
        //loadingText = GameObject.FindGameObjectWithTag("Loading").GetComponent<Text>();
        mobCounter = GameObject.FindGameObjectWithTag("EnemyCounter").GetComponent<Text>();
        timeCounter = GameObject.FindGameObjectWithTag("TimeCounter").GetComponent<Text>();
        //UIButtons = GameObject.FindGameObjectWithTag("UIButtons");
        //UIButtons.SetActive(false);
        //startButton.onClick.AddListener(StartNewGame);
        //quitButton.onClick.AddListener(QuitGame);
    }

    private void Update()
    {
        int timer = (int) Time.timeSinceLevelLoad;
        if(countingTime)
        {
            timeCounter.text = "Time: " + (timer / 60).ToString("D2") + ":" + ((timer % 3600) % 60).ToString("D2");
        }
    }

    public void UpdateHealthBar()
    {
        float ratio = health / maxHealth;
        currentHealth.rectTransform.localScale = new Vector3(ratio, 1, 1);
        healthText.text = health + "/" + maxHealth;
    }

    public void UpdateMobCounter(int change)
    {
        mobCount += change;
        if(mobCounter != null)
        {
            mobCounter.text = "Bears: " + mobCount;
        }
        if(mobCount == 0)
        {
            ShowWinScreen();
        }
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
        controlPanel.SetActive(false);
        countingTime = false;
        /*foreach(GameObject ds in deathScreen)
        {
            ds.GetComponent<Text>().enabled = true;
        }*/
        pauseText.text = "WASTED";
        pauseText.color = Color.red;
        pauseText.GetComponent<Outline>().effectColor = Color.black;
        resumeButton.interactable = false;
        menuPanel.SetActive(true);

        //UIButtons.SetActive(true);
    }

    public void ShowWinScreen()
    {
        controlPanel.SetActive(false);
        countingTime = false;
        /*foreach(GameObject ws in winScreen)
        {
            ws.GetComponent<Text>().enabled = true;
        }*/
        winPanel.SetActive(true);

        winPanel.transform.GetChild(1).GetComponent<Button>().Select();

        pauseText.text = "GOOD JOB!";
        pauseText.color = Color.green;
        pauseText.GetComponent<Outline>().effectColor = Color.black;
        //UIButtons.SetActive(true);
    }
    /*
    public void ShowLoading(bool show)
    {
        loadingText.enabled = show;
    }
    */
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

    public Image GetAttackCooldownImage()
    {
        Image attackCooldownImage = controlPanel.transform.Find("Attack Button").Find("Cooldown").GetComponent<Image>();
        if(attackCooldownImage == null)
        {
            Debug.LogError("Could not find UI -> \"Attack Button\" -> \"Cooldown\", check names!");
        }
        return attackCooldownImage;
    }

    public Image GetSwapCooldownImage()
    {
        Image swapCooldownImage = controlPanel.transform.Find("Swap wep Button").Find("Cooldown").GetComponent<Image>();
        if (swapCooldownImage == null)
        {
            Debug.LogError("Could not find UI -> \"Swap wep Button\" -> \"Cooldown\", check names!");
        }
        return swapCooldownImage;
    }

    public Button GetAttackButton()
    {
        Button attackButton = controlPanel.transform.Find("Attack Button").GetComponent<Button>();
        if (attackButton == null)
        {
            Debug.LogError("Could not find UI -> \"Attack Button\", check names!");
        }
        return attackButton;
    }

    public Button GetSwapButton()
    {
        Button swapButton = controlPanel.transform.Find("Swap wep Button").GetComponent<Button>();
        if (swapButton == null)
        {
            Debug.LogError("Could not find UI -> \"Swap wep Button\", check names!");
        }
        return swapButton;
    }
}
