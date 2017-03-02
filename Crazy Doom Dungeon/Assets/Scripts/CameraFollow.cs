using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    [SerializeField]
    protected Transform target;
    protected Vector3 offset;
    private Vector3 vel = Vector3.zero;
    
    public void Follow(Transform target)
    {
        this.target = target;
    }

    void Start()
    {
        offset = transform.position - target.transform.position;
    }

	void Update () {
        Vector3 movetarget = target.transform.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, movetarget,ref vel, 0f);	
	}
}
