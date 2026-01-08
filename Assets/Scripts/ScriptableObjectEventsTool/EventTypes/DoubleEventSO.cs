using System;
using UnityEngine;

[CreateAssetMenu(
    menuName = "SriptableObjectEvents/Double Event",
    fileName = "NewDoubleEvent"
)]
public class DoubleEventSO : ScriptableObject
{
    private event Action<double> listeners;
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
