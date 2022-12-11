using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeactivateWall : MonoBehaviour
{
     public void Deactivate() => gameObject.SetActive(false);

     private void Update()
     {
          if (Game_Manager.instance.InCombat)
          {
               Deactivate();
          }
     }
}
