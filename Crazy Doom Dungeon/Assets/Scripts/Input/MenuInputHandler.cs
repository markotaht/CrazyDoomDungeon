using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuInputHandler : AInputHandler {

    [SerializeField]
    private GameObject menuPanel;
    // Use this for initialization
    void Start () {
        menuPanel.transform.GetChild(1).GetComponent<Button>().Select();
    }
}
