using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRecolectionObjects : MonoBehaviour
{
    private int _Check;
    [SerializeField] private int _maxChecks;
    [SerializeField] private ChangeScenes changeScenes;

    public void BoolsChecks()
    {
        _Check++;
        Debug.Log("checking");
    }
    public void CheckReadytoGO()
    {
        if(_Check >= _maxChecks)
        {
            changeScenes.reaadyToGO = true;
        }
    }
}
