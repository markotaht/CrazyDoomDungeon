using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour {

    public static TutorialController instance;
    [SerializeField]
    private GameObject[] tutorial_steps;

	// Use this for initialization
	void Start () {
        instance = this;
        tutorial_steps[0].SetActive(true);
	}

    public void NextStep(int step)
    {
        tutorial_steps[step].SetActive(false);
        tutorial_steps[step+1].SetActive(true);
    }
}
