using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    public void Attack(Vector2 Dir)
    {
        GameObject p = Instantiate(projectilePrefab, transform.position,quaternion.identity);
        p.GetComponent<PlayerProjectile>().dir = Dir;
    }
    
}
