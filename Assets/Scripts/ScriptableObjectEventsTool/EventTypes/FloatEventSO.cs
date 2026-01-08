using System;
using UnityEngine;

public class FloatEventSO : ScriptableObject
{
    private event Action<float> listeners;

    [SerializeField]
    private float value;
    public void Trigger(float newvalue)
    {
        value = newvalue;
        listeners?.Invoke(value);
    }

    public void AddListener(Action<float> listener)
    {
        listeners += listener;
    }

    public void RemoveListener(Action<float> listener)
    {
        listeners -= listener;
    }

    public float GetValue() { return value; }
}