using System;
using UnityEngine;
public class GameObjectEventSO : ScriptableObject
{
    private event Action<GameObject> listeners;
    
    [SerializeField]
    private GameObject value;

    public void Trigger(GameObject newValue)
    {
        value = newValue;
        listeners?.Invoke(value);
    }

    public void AddListener(Action<GameObject> listener)
    {
        listeners += listener;
    }

    public void RemoveListener(Action<GameObject> listener)
    {
        listeners -= listener;
    }

    public GameObject GetValue()
    {
        return value;
    }
}