using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : Command {
	
    public MoveCommand()
    {

    }

    public override void Execute()
    {
        throw new NotImplementedException();
    }

    public override void Execute(Transform target, PlayerController controller)
    {
        throw new NotImplementedException();
    }

    public override void Execute(Vector3 target, PlayerController controller)
    {
        controller.Move(target);
    }

    public override void Execute(Vector2 diff, PlayerController controller)
    {
        controller.Move(diff);
    }

}
