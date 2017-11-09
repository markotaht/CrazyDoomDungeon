using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ABaseAI : MonoBehaviour {


    protected bool Alive = true;

    [SerializeField]
    private GameObject targetIndicator;

    public bool isAlive()
    {
        return Alive;
    }

    public void Target(bool targeted)
    {
        targetIndicator.SetActive(targeted);
    }

    public abstract bool WasHit(float wepStrength);
}
