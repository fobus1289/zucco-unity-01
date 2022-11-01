using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManagerScript : MonoBehaviour
{

    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private List<Vector3> positions;
    [SerializeField] private Slider Slider;
    public static GameManagerScript Instance;
    public static List<Enemy> Enemies = new();
    [SerializeField] private CinemachineFreeLook _cinemachine;
    private void Awake()
    {
        var player = Container.SelectPlayer;
        player.Enable();
        player.GetComponent<PlayerScript>()._cinemachineFreeLook = _cinemachine;
        player.GetComponent<PlayerScript>().Slider = Slider;
        _cinemachine.Follow = player.transform;
        _cinemachine.LookAt = player.transform;

        StartCoroutine(SavePlayerInfo(player.GetComponent<PlayerScript>()));
    }

    private IEnumerator SavePlayerInfo(PlayerScript playerScript)
    {
        var _currentPlayerInfo = Container.PlayerInfo;
        var playerName = _currentPlayerInfo.name;
        var fileName = _currentPlayerInfo.fileName;
        var playerTransform = playerScript.transform;
        while (true)
        {
            var currentPlayerInfo = Container.PlayerInfo;
            var playerPosition = playerTransform.position;
            
            var playerInfo = new PlayerInfo
            {
                name = playerName,
                fileName = fileName,
                x = playerPosition.x,
                y = playerPosition.y,
                z = playerPosition.z,
                maxHp = playerScript.HP
            };
            
            playerInfo.Serialize(fileName);
            Container.PlayerInfo = playerInfo;
            yield return new WaitForSeconds(5F);
        }
    }
    
    private void Start()
    {
        
        Instance = this;
        
        foreach (var position in positions)
        {
            var newEnemy = Instantiate(enemyPrefab,position, Quaternion.identity);
            newEnemy.Slider = Slider;
            newEnemy.SetHP(Random.Range(50,150));
            
            Enemies.Add(newEnemy);
        }
        
    }
    
    public void SetSpawn(ISpawn spawn)
    {
        spawn.DeSpawn();
        StartCoroutine(Spawn(spawn));
    }

    private IEnumerator Spawn(ISpawn spawn)
    {
        yield return new WaitForSeconds(spawn.RespawnTime());
        spawn.GetGameObject().SetActive(true);
        spawn.Spawn();
    }
    
}
