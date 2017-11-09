using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetachParticle : MonoBehaviour
{

    public ParticleSystem particles;

    // Use this for initialization
    private void OnDestroy()
    {
        //TODO: Not working as intended: after detaching the old particles are instantly destroyed

        //transform.DetachChildren();
        particles.transform.parent = null;
        //particles.Stop(false, ParticleSystemStopBehavior.StopEmitting);
        var em = particles.emission;
        em.rateOverTime = 0;
        Destroy(particles.transform.gameObject, 0.5f);
    }
}
