using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    private GameMangerUIScript _gameMangerUIScript;
    
    private void Start()
    {
        _gameMangerUIScript = FindObjectOfType<GameMangerUIScript>();
    }
    
    public void ActionStart()
    {
        var currentPlayer = _gameMangerUIScript._currentPlayer;
        
        if (currentPlayer != null)
        {
            var tmpInputField =  _gameMangerUIScript.tmpInputField;
            var text = tmpInputField.text;
            
            var playerInfo = new PlayerInfo
            {
                name = text,
                fileName = text,
                maxHp = 500,
            };

            playerInfo.Serialize($"{text}.save");
            
            Container.SelectPlayer = currentPlayer;
            Container.PlayerInfo = playerInfo;
            
            DontDestroyOnLoad(Container.SelectPlayer.gameObject);
            SceneManager.LoadScene(1);
        }
    }
    
}
