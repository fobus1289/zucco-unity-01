using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerControllerScript : MonoBehaviour
{ 
    [SerializeField] private PlayerScript playerScript;
    [SerializeField] private Target target;
    [SerializeField] private PlayerInputScript playerInputScript;
    
    private void Start()
    {
        playerScript = GetComponent<PlayerScript>();
        target = GetComponent<Target>();
        playerInputScript = GetComponent<PlayerInputScript>();
    }
    
    public void Enable()
    {
        playerScript.enabled = true;
        target.enabled = true;
        playerInputScript.enabled = true;
    }
    
}
