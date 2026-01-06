using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{


    public GameObject bulletSpawnPoint;
    public GameObject Bullet;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnBullet() { 
        Instantiate(Bullet,bulletSpawnPoint.transform.position,Quaternion.identity);
    }
}
