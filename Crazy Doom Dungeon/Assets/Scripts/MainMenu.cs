using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public Button startButton;
    public Button quitButton;
    public Button grindButton;
    public Button tutorialButton;

    // Use this for initialization
    void Start () {
        startButton.onClick.AddListener(StoryMode);
        tutorialButton.onClick.AddListener(Tutorial);
        grindButton.onClick.AddListener(StartNewGame);
        quitButton.onClick.AddListener(QuitGame);
	}
	
    private void StoryMode()
    {
        Time.timeScale = 1;
        Debug.Log("Story mode");
        SceneManager.LoadScene("Story");
    }

    private void StartNewGame()
    {
        Time.timeScale = 1;
        Debug.Log("New Game");
        SceneManager.LoadScene("Diana");
    }

    private void Tutorial()
    {
        Time.timeScale = 1;
        Debug.Log("Tutorial");
        SceneManager.LoadScene("Tutorial");
    }

    private void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    } 
}
