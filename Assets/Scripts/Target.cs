using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour,ITakeDamage
{
    public int HP = 0;

    public bool die => HP <= 0;

    public Slider slider;

    private void Start()
    {
        slider.maxValue = HP;
        slider.value = HP;
    }


    public void TakeDamage(int damage)
    {
        HP -= damage;
        slider.value = HP;
        print("TakeDamage");
        if (die)
        {
            print("I die");
        }
    }

}
