using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidEventSO : ScriptableObject
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
