using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour {

    [SerializeField]
    public PlayerController currentActor;

    private Vector3 target;
    private MoveCommand move = new MoveCommand();
    private AttackCommand attack = new AttackCommand();
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit,100);

        target = hit.point;
        if (Input.GetMouseButton(0))
        {
            //TODO Kuidagi tea midagi olukorras kus vajutatakse kaardilt välja....
            if (hit.collider == null || hit.collider.gameObject.tag != "Enemy")
            {
                move.Execute(target, currentActor);
            }
            else
            {
                attack.Execute(hit.collider.gameObject.transform.position, currentActor);
            }
        }
        if (Input.GetMouseButton(1))
        {
            //TODO Special attack
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            move.Execute(currentActor.transform.position, currentActor);
        }
	}

    public void setPlayer(PlayerController controller)
    {
        currentActor = controller;
    }
}
