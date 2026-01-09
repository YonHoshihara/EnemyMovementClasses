using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeContrroller : MonoBehaviour
{
    [SerializeField]
    private int _maxLife;

    [SerializeField]
    private int _currentLife;



    void Start()
    {
        _currentLife = _maxLife;
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void GetDamage(int damage)
    {
        
        _currentLife -= damage;
        if(_currentLife <= 0){

        }
    }
}
