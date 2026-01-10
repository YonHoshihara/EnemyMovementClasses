using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemyAnimationEventsController : MonoBehaviour
{
  [SerializeField]
  private ShooterEnemyController controller;

  public void SpawnBullet()
  {
        controller.SpawnFireball();
  }
}
