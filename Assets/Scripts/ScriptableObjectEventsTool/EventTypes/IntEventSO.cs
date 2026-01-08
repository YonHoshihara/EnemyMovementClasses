using System;
using UnityEngine;

[CreateAssetMenu(
    menuName = "SriptableObjectEvents/Int Event",
    fileName = "NewIntEvent"
)]
public class IntEventSO : ScriptableObject
{
    private event Action<int> listeners;
    private int value;
    public void Trigger(int newvalue)
    {
        value = newvalue;
        listeners?.Invoke(value);
    }

    public void AddListener(Action<int> listener)
    {
        listeners += listener;
    }

    public void RemoveListener(Action<int> listener)
    {
        listeners -= listener;
    }

    public int GetValue() { return value; }
}