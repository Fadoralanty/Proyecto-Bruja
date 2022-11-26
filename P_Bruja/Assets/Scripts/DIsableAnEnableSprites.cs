using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DIsableAnEnableSprites : MonoBehaviour
{
    [SerializeField] private GameObject _hideSprite;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            _hideSprite.SetActive(false);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            _hideSprite.SetActive(true);
        }
    }
}
