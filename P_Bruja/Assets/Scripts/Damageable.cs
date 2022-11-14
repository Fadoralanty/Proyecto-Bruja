using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    [SerializeField] protected float _maxLife;
    [SerializeField] protected float _currentLife;
    [SerializeField] Animator _anim;
    public float CurrentLife => _currentLife;
    public float MaxLife => _maxLife;
    public Action<float> onLifeChange;//HP y barra de vida
    public UnityEvent onDie = new UnityEvent();

    protected virtual void Awake()
    {
        _anim = GetComponent<Animator>();
        ResetValues();
    }
    
    public virtual void GetDamage(float damage)
    {
        if (_currentLife > 0 )
        {
            _anim.SetBool("Hit", true);
            _currentLife -= damage;
        }
        onLifeChange?.Invoke(_currentLife);
        if (_currentLife <= 0)
        {
            DieHandler();
        }
    }
    public virtual void GetHealing(float healNum)
    {
        _currentLife += healNum;
        if (_currentLife > _maxLife)
        {
            _currentLife = _maxLife;

        }
        onLifeChange?.Invoke(_currentLife);

    }

    public void SetLife(float life)
    {
        _currentLife = life > _maxLife ? _maxLife : life;
    }
    public void DieHandler()
    {
        onDie.Invoke();
    }
    public void ResetValues()
    {
        _currentLife = _maxLife;
        onLifeChange?.Invoke(_currentLife);
    }
    public float GetLifePercentage()
    {
        return (float)_currentLife / _maxLife;
    }
}
