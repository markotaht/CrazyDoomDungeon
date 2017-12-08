using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropController : MonoBehaviour {
    
    [SerializeField]
    private GameObject[] drops;
    [SerializeField]
    private float[] droprate;

    public void DropSomething()
    {
        for (int i = 0; i < drops.Length; i++)
        {
            float rnd = Random.Range(0.0f, 1.0f);
            if(rnd <= droprate[i])
            {
                GameObject go =Instantiate(drops[i], transform.position, drops[i].transform.rotation);

                if (gameObject.GetComponent<ABaseAI>() == null)
                {
                    go.AddComponent<TutorialStep>();
                    go.GetComponent<TutorialStep>().tutorial_step = 2;
                }
            }
        }
    }
}
