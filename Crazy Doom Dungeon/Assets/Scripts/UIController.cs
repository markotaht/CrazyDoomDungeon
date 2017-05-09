using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    [SerializeField]
    private Slider hp;

    public void updateHealth(float life)
    {
        hp.value = life;
    }

    public void gotHit()
    {
        StartCoroutine(Flash());
    }

    IEnumerator Flash()
    {
        Debug.Log(GameObject.FindGameObjectWithTag("Hit"));
        Image hit = GameObject.FindGameObjectWithTag("Hit").GetComponent<Image>();
        Debug.Log(hit);
        hit.enabled = true;
        yield return new WaitForSeconds(0.2f);
        hit.enabled = false;
    }
}
