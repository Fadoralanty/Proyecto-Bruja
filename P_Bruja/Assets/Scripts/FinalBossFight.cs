using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossFight : MonoBehaviour
{
    [SerializeField] private NpcDialogueTrigger trigger;
    [SerializeField] private GameObject witch;
    [SerializeField] private HolyWater _holyWater;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(trigger.spookyThing == true)
        {
            witch.SetActive(true);
            Game_Manager.instance.InCombat = true;
           _holyWater.StartingTeleport();
        }
    }
}
