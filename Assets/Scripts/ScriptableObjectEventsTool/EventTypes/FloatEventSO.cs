using System;
using UnityEngine;

[CreateAssetMenu(
    menuName = "SriptableObjectEvents/Float Event",
    fileName = "NewIntEvent"
)]
public class FloatEventSO : ScriptableObject
{
    private event Action<float> listeners;
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