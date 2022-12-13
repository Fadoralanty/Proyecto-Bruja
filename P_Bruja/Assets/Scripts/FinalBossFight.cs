using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalBossFight : MonoBehaviour
{
    [SerializeField] private NpcDialogueTrigger trigger;
    [SerializeField] private GameObject witch;
    [SerializeField] private EnemyController _lifeWitch;
    [SerializeField] private GameObject Darkness;
    [SerializeField] private HolyWater _holyWater;
    private bool ready = true;
    private bool _deadWitch = true;

    void Update()
    {
        if(trigger.spookyThing == true && ready == true)
        {
            StartCoroutine(Start3());
            ready = false;
        }
        if(_lifeWitch._imDead == true && _deadWitch == true)
        {
            StartCoroutine(Start4());
            _deadWitch = false;
        }
    }

    IEnumerator Start4()
    {
        yield return new WaitForSeconds(3);
        Darkness.SetActive(false);
        int morality = Game_Manager.instance._morality;
        if (morality >= 1)
        {
            SceneManager.LoadScene("Good Ending");
        }
        if (morality <= 0)
        {
            SceneManager.LoadScene("Bad Ending");
        }
    }
    IEnumerator Start3()
    {
        Darkness.SetActive(true);
        witch.SetActive(true);
        _holyWater.StartingTeleport();
        AudioManager.instance.play("Boss Theme");
        AudioManager.instance.Stop("bg");
        yield return null;
    }
}
