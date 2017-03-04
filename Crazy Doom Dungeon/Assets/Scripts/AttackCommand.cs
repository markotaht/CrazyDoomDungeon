using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCommand : Command {
    
    // Use this for initialization
    void Start () {
		
	}

    public override void Execute()
    {
        throw new NotImplementedException();
    }

    public override void Execute(Vector3 target, PlayerController controller)
    {
        controller.Attack(target);
    }
}
