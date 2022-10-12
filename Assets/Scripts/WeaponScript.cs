using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WeaponScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var component = other.GetComponent<ITakeDamage>();
        if (component != null)
        {
            component.TakeDamage(Random.Range(2,15));
        }
    }
}
