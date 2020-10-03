using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class ScriptableReference<T> : ScriptableObject
{
    [SerializeField] private T _value;

    public T Value
    {
        get => _value;
        set => _value = value;
    }
}