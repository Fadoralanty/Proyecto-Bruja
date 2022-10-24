using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("Main HealthBar")]
    [SerializeField] private Image _healthBar;

    [Header("Sub HealhBar")]
    [SerializeField] private Image _healthBarDelay;
    [SerializeField] private float _loseHealthSpeed= 0.2f;

    [Header("Life Components")]
    [SerializeField] private Damageable _damageable;


    private void Awake()
    {
        _damageable.onLifeChange += FillHealthbar;
    }
    
    private void Update()
    {
        Fill2ndHealthbar();
    }
    public void FillHealthbar(float currentlife)
    {
        _healthBar.fillAmount = (float)currentlife/_damageable.MaxLife;
    }   
    public void Fill2ndHealthbar()
    {
        if (_healthBarDelay.fillAmount>_healthBar.fillAmount)
        {
            _healthBarDelay.fillAmount -= _loseHealthSpeed  * Time.deltaTime;
        }
        else
        {
            _healthBarDelay.fillAmount = _healthBar.fillAmount;
        }
    }

}
