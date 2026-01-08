using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    menuName = "SriptableObjectEvents/Void Event",
    fileName = "NewGameObjectEvent"
)]

public class VoidEventSO : MonoBehaviour
{
    private event Action listeners;

    public void Trigger()
    {
        listeners?.Invoke();
    }

    public void AddListener(Action listener)
    {
        listeners += listener;
    }

    public void RemoveListener(Action listener)
    {
        listeners -= listener;
    }

}
