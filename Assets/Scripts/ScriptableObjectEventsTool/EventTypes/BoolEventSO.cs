using System;
using UnityEngine;

[CreateAssetMenu(
    menuName = "SriptableObjectEvents/Bool Event",
    fileName = "NewBoolEvent"
)]
public class BoolEventSO : ScriptableObject
{
    private event Action<bool> listeners;
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
