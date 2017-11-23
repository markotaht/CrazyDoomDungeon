using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStep : MonoBehaviour {

    public int tutorial_step = 0;

    [SerializeField]
    private bool hit = false;

    private void OnTriggerEnter(Collider other)
    {
        //   if(other.tag == "Player")
        //   {

        if (!hit && other.tag == "Player" || hit && other.tag != "Player")
        {
            TutorialController.instance.NextStep(tutorial_step);
            Destroy(this);
        }

     //   }
    }
}
