using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    [SerializeField]
    private Slider hp;
    private Image hit;

    private void Start()
    {

        //Debug.Log(GameObject.FindGameObjectWithTag("Hit"));
        hit = GameObject.FindGameObjectWithTag("Hit").GetComponent<Image>();
    }

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
        //Debug.Log(hit);
        hit.enabled = true;
        yield return new WaitForSeconds(0.2f);
        hit.enabled = false;
    }
}
