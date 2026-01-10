using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LifeContrroller : MonoBehaviour
{

    [SerializeField]
    private int _maxLife;

    [SerializeField]
    private int _currentLife;

    public UnityEvent onGetDamage;
    
    public UnityEvent onDeath;
    void Start()
    {
        _currentLife = _maxLife;
    }

    public void GetDamage(int damage)
    {

        _currentLife -= damage;
        if (_currentLife <= 0) {
            onDeath?.Invoke();
        }
        else
        {
            onGetDamage?.Invoke();
        }
    }

    public void RegenLife(int life)
    {
        _currentLife += life;
    }
}
