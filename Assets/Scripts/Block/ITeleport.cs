using UnityEngine;

public interface ITeleport
{
    bool IsTeleportingWhenOutOfRange { get; }
    Vector2 Direction { get; }
}