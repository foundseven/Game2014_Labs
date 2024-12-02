using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    [SerializeField]
    int _maxHealth;

    [SerializeField]
    bool _gameOverCondition;

    int _value;

    Slider _slider;
    // Start is called before the first frame update
    void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.maxValue = _maxHealth;
        _slider.value = _maxHealth;
    }

    public void ResetHealthBar()
    {
        _slider.maxValue = _maxHealth;
        _slider.value = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _slider.value -= damage;
        if(_slider.value <= 0)
        {
            //player dies lose one life

            if(_gameOverCondition == true)
            {
                //game over
            }
        }
    }

    public void HealHealth(int healAmount)
    {
        _slider.value += healAmount;

        if(_slider.value >= _maxHealth)
        {
            _slider.value = _maxHealth;
        }
    }
}
