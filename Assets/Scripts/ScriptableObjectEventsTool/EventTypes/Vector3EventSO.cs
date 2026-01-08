using System;
using UnityEngine;

[CreateAssetMenu(
    menuName = "SriptableObjectEvents/Vector3 Event",
    fileName = "NewVector3Event"
)]
public class Vector3EventSO : ScriptableObject
{
    private event Action<Vector3> listeners;
    private Vector3 value;

    public void Trigger(Vector3 newValue)
    {
        value = newValue;
        listeners?.Invoke(value);
    }

    public void AddListener(Action<Vector3> listener)
    {
        listeners += listener;
    }

    public void RemoveListener(Action<Vector3> listener)
    {
        listeners -= listener;
    }

    public Vector3 GetValue()
    {
        return value;
    }
}
