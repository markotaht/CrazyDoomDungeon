using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    [SerializeField]
    public Transform target;

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

    public void setTarget(Transform target)
    {
        this.target = target;

        Vector3 dir = Quaternion.Euler(30f,45f,0f) * Vector3.back;
        Vector3 position = target.transform.position + dir * 9;
        Debug.Log(position);
        transform.position = position;
        //offset = transform.position - target.transform.position;
    }
}
