using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour {

    public static TutorialController instance;
    [SerializeField]
    private GameObject[] tutorial_steps;

    private int currentStep = 0;
	// Use this for initialization
	void Start () {
        instance = this;
        tutorial_steps[currentStep].SetActive(true);
	}

    public void NextStep()
    {
        Debug.Log("here");
        tutorial_steps[currentStep].SetActive(false);
        tutorial_steps[++currentStep].SetActive(true);
    }
}
