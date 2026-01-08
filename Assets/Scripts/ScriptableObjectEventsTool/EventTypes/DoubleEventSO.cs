using System;
using UnityEngine;

public class DoubleEventSO : ScriptableObject
{
    private event Action<double> listeners;
    
    [SerializeField]
    private double value;
    public void Trigger(double newValue)
    {
        value = newValue;
        listeners?.Invoke(value);
    }

    public void AddListener(Action<double> listener)
    {
        listeners += listener;
    }

    public void RemoveListener(Action<double> listener)
    {
        listeners -= listener;
    }

    public double GetValue()
    {
        return value;
    }
}
