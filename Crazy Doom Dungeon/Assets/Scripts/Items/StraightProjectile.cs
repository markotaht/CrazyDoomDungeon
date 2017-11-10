using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightProjectile : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float damage;
    [SerializeField]
    private float maxDistance;

    public ParticleSystem particles;

    private Vector3 direction;
    private bool attacking = false;
    private float distanceTravelled = 0;
    
    // Use this for initialization
    void Awake()
    {
        attacking = false;
        distanceTravelled = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (attacking && distanceTravelled < maxDistance)
        {
            distanceTravelled += Time.deltaTime * speed * 10;
            transform.position = transform.position + Time.deltaTime * speed * direction;
        }
        else if (distanceTravelled >= maxDistance)
        {
            particles.Stop();
            particles.gameObject.transform.SetParent(null);
            Destroy(particles.transform.gameObject, 0.5f);
            Destroy(gameObject);
        }
    }

    public void attack(Vector3 targetPos)
    {
        attacking = true;
        direction = (targetPos - transform.position).normalized;
        direction.y = 0;
        direction = direction.normalized;
    }

    private void OnTriggerEnter(Collider other)
    {
        ABaseAI enemy = other.GetComponent<ABaseAI>();
        if (enemy)
        {
            enemy.WasHit(damage);
        }
    }
}
