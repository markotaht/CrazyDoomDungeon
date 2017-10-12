using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCGameManager : AGameManager {
    private KeyCode forward = KeyCode.W;
    private KeyCode backward = KeyCode.S;
    private KeyCode left = KeyCode.A;
    private KeyCode right = KeyCode.D;

    public override Vector2 MovementDirection()
    {
        Vector2 dir = new Vector2(0,0);

        if (Input.GetKey(forward))
        {
            dir.y = 1;
        }
        else if (Input.GetKey(backward))
        {
            dir.y = -1;
        }

        if (Input.GetKey(right))
        {
            dir.x = 1;
        }
        else if (Input.GetKey(left))
        {
            dir.x = -1;
        }

        return dir;
    }
}
