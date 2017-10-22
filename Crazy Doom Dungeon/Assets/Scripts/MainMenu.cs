using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public Button startButton;
    public Button quitButton;

    // Use this for initialization
    void Start () {
        startButton.onClick.AddListener(StartNewGame);
        quitButton.onClick.AddListener(QuitGame);
	}
	
    private void StartNewGame()
    {
        Time.timeScale = 1;
        Debug.Log("New Game");
        SceneManager.LoadScene("Diana");
    }

    private void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

	// Update is called once per frame
	void Update () {
		
	}   
}
