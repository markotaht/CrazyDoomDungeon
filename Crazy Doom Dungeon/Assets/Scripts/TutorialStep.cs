using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStep : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
     //   if(other.tag == "Player")
     //   {
            TutorialController.instance.NextStep();

        Destroy(this);
     //   }
    }
}
