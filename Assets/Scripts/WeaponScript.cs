using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WeaponScript : MonoBehaviour
{

    [SerializeField] private int damage;


    private void Start()
    {
        if (damage == 0)
        {
            damage = Random.Range(15, 35); 
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        var component = other.GetComponent<ITakeDamage>();
        if (component != null)
        {
            component.TakeDamage(damage);
        }
    }

}
