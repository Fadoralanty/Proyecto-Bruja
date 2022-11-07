using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//https://www.youtube.com/watch?v=XOjd_qU2Ido&t=183s
//serializacion binaria
[System.Serializable]
public class PlayerDATA 
{
    public float currentLife;
    public float[] position;

    public PlayerDATA(PlayerController player)
    {
        currentLife = player.Damageable.CurrentLife;
        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
        
    }
}