using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputScript : MonoBehaviour
{

    private List<WeaponScript> _weaponScripts;
    private PlayerScript _playerScript;
    private List<KeyCode> _keyCodes = new();
    private void Start()
    {
        _playerScript = GetComponent<PlayerScript>();
        _weaponScripts = _playerScript.Weapons;
        
        for (var i = 0; i < _weaponScripts.Count; i++)
        {
            _keyCodes.Add((KeyCode)(282+i));
        }
        
    }

    private void Update()
    {
        if (Input.anyKey)
        {
            for (var i = 0; i < _keyCodes.Count; i++)
            {
                if (Input.GetKeyDown(_keyCodes[i]))
                {
                    _playerScript.ChangeWeapon(i);
                    break;
                }
            }
        }
    }
    
}
