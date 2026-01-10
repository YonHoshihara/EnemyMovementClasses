using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEventController : MonoBehaviour
{
   [SerializeField]
   private PlayerController controller;
  public void OnAttackEnd()
   {
        controller.OnAttackAnimationFinished();
   }
}
