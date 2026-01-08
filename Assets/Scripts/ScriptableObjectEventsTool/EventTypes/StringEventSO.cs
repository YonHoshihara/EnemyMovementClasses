using System;
using UnityEngine;

public class StringEventSO : ScriptableObject
{
    private event Action<string> listeners;
    
    [SerializeField]
    private string value;
    public void Trigger(string newValue)
    {
        value = newValue;
        listeners?.Invoke(value);
    }

    public void AddListener(Action<string> listener)
    {
        listeners += listener;
    }

    public void RemoveListener(Action<string> listener)
    {
        listeners -= listener;
    }

    public string GetValue()
    {
        return value;
    }
}
