using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour,ITakeDamage
{
    public int HP = 0;

    public bool die => HP <= 0;
    
    
    public void TakeDamage(int damage)
    {
        HP -= damage;
        
        if (die)
        {
            print("I die");
        }
    }

}
