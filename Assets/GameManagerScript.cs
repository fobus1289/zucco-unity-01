using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{

    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private List<Vector3> positions;
    [SerializeField] private Slider Slider;
    public static GameManagerScript Instance;
    public static List<Enemy> Enemies = new();
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
