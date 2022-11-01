using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameMangerUIScript : MonoBehaviour
{

    [SerializeField] private Transform playerPanel;
    [SerializeField] private Button playerBtnPrefab;
    [SerializeField] private List<PlayerControllerScript> players;
    public TMP_InputField tmpInputField;
    public PlayerControllerScript _currentPlayer { private set; get; }
    
    private void Awake()
    {
        var a= Container.Deserialize("user4");
        print(a);
        foreach (var player in players)
        {
           var currentBtn = Instantiate(playerBtnPrefab, playerPanel);
            
           currentBtn.transform.SetParent(playerPanel);

           currentBtn.GetComponentInChildren<TextMeshProUGUI>().SetText(player.name);
           
           currentBtn.onClick.AddListener(() =>
           {
               if (_currentPlayer)
               {
                   Destroy(_currentPlayer.gameObject);
               }
               
               _currentPlayer = Instantiate(player,
                   new Vector3(0,0,-6.5F)
               ,new Quaternion(0,-180F,0,0));

           });
        }
    }
}
