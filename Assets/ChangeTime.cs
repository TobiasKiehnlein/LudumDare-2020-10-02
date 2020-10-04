using UnityEngine;

public class ChangeTime : MonoBehaviour
{
    public void SetTimeSpeed(float speed)
    {
        Time.timeScale = speed;
    }
}