using System;
using UnityEngine;

[CreateAssetMenu(
    menuName = "SriptableObjectEvents/GameObject Event",
    fileName = "NewGameObjectEvent"
)]
public class GameObjectEventSO : ScriptableObject
{
    private event Action<GameObject> listeners;
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
