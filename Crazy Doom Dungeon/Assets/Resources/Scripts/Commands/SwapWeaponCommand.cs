using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapWeaponCommand : Command {

    public SwapWeaponCommand()
    {

    }

    public override void Execute()
    {
    }

    public override void Execute(Transform target, PlayerController controller)
    {
        throw new NotImplementedException();
    }

    public override void Execute(Vector3 target, PlayerController controller)
    {
        controller.SwapWeapon();
    }
}
