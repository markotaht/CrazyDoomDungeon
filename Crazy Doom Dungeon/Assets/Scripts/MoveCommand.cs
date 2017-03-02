using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : Command {

    private Vector3 direction;
	
    public MoveCommand(Vector3 direction)
    {
        this.direction = direction;
    }

    public override void Execute()
    {
        throw new NotImplementedException();
    }
}
