using UnityEngine;

public enum TimeDirection
{
    Forward,
    Backward,
    Idle
}

[CreateAssetMenu(menuName = "Game/Scriptable Reference/TimeDirection Reference")]
public class TimeDirectionReference : ScriptableReference<TimeDirection>
{
}