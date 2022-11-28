using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossFight : MonoBehaviour
{
    [SerializeField] private NpcDialogueTrigger trigger;
    [SerializeField] private GameObject witch;
    [SerializeField] private GameObject Darkness;
    [SerializeField] private HolyWater _holyWater;
    private bool ready = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(trigger.spookyThing == true && ready == true)
        {
            StartCoroutine(Start3());
            ready = false;
        }
    }

    IEnumerator Start3()
    {
        witch.SetActive(true);
        _holyWater.StartingTeleport();
        Darkness.SetActive(true);
        yield return null;
    }
}
