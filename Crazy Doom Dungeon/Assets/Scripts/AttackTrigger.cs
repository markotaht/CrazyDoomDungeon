using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour {

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<Renderer>().material.color = Color.red;
            other.gameObject.GetComponent<BasicAI>().Die();
        }
    }
}
