using UnityEngine;

public abstract class Command{
    public abstract void Execute();
    public abstract void Execute(Vector3 target, PlayerController controller);
}
