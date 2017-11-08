using UnityEngine;

public abstract class Command{
    public abstract void Execute();
    public abstract void Execute(Vector3 target, PlayerController controller);
    public abstract void Execute(Transform target, PlayerController controller);
    public virtual void Execute(Vector2 diff, PlayerController controller) { }
}
