using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemObtained : MonoBehaviour
{
    public TextMeshProUGUI text;

    private void Start()
    {
        Destroy(gameObject,1f);
    }

    private void Update()
    {
        transform.position += Vector3.up * Time.deltaTime;
    }
}
