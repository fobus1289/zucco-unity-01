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
        Container.SelectPlayer = _gameMangerUIScript._currentPlayer;
        
        if (Container.SelectPlayer != null)
        {
            DontDestroyOnLoad(Container.SelectPlayer.gameObject);
            SceneManager.LoadScene(1);
        }
    }
    
}
