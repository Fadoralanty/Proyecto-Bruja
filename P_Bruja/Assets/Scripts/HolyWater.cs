using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyWater : MonoBehaviour
{
    [SerializeField] private Damageable _witch;
    [SerializeField] private List<Vector3> _transformsPositions;
    [SerializeField] private GameObject _darkness;
    private int allObjects;
    private int _index;

    public void StartingTeleport()
    {
            StartCoroutine(Start());
    }

    IEnumerator Start()
    {
        Teleport();
        yield return new WaitForSecondsRealtime(3);
        StartCoroutine(Start2());
    }
    IEnumerator Start2()
    {
        Teleport();
        yield return new WaitForSecondsRealtime(3);
        StartCoroutine(Start());
    }
    void Teleport()
    {
        _index = Random.Range(0, _transformsPositions.Count);
        transform.position = _transformsPositions[_index];
    }
    void Vulnerability()
    {
        _witch.DeactivateInmortality();
    }
    void CheckVulnerability()
    {
        if(allObjects >= 2)
        {
            Vulnerability();
            StopAllCoroutines();
            _darkness.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemWorld itemWorld = collision.GetComponent<ItemWorld>();
        if (itemWorld != null)
        {
            if (collision.CompareTag("BoneOne"))
            {
                //gameObject.transform = _transformsPositions[Random.Range(0, _transformsPositions.Count)];
                itemWorld.DestroySelf();
                allObjects++;
                CheckVulnerability();
            }
            if (collision.CompareTag("Bones"))
            {
                itemWorld.DestroySelf();
                allObjects++;
                CheckVulnerability();
            }
        }
    }
}
