using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    public void Attack(Quaternion rotation)
    {
        Instantiate(projectilePrefab, transform.position, rotation);
    }
    
}
