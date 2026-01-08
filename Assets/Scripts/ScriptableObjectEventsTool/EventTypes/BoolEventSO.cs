using System;
using UnityEngine;

public class BoolEventSO : ScriptableObject
{
    private event Action<bool> listeners;
    [SerializeField]
    private bool value;

    public void Trigger(bool newValue)
    {
        value = newValue;
        listeners?.Invoke(value);
    }

    public void AddListener(Action<bool> listener)
    {
        listeners += listener;
    }

    public void RemoveListener(Action<bool> listener)
    {
        listeners -= listener;
    }

    public bool GetValue()
    {
        return value;
    }
}
