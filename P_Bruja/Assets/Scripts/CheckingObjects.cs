using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckingObjects : MonoBehaviour
{
    [SerializeField] CheckRecolectionObjects _checkObs;
    private Collider2D _collider;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _checkObs.BoolsChecks();
            Debug.Log("1");
            _checkObs.CheckReadytoGO();
            _collider.enabled = false;
        }
    }
}
