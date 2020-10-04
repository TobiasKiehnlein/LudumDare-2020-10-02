using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTime : MonoBehaviour
{
    public void setTimeSpeed(float speed)
    {
        Time.timeScale = speed;
    }
}