using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{

    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private List<Vector3> positions;

    public static GameManagerScript Instance;
    
    private void Start()
    {
        Instance = this;
        
        foreach (var position in positions)
        {
            var newEnemy = Instantiate(enemyPrefab,position, Quaternion.identity);
        }
        
    }
    
    public void SetSpawn(ISpawn spawn)
    {
        spawn.DeSpawn();
        
        StopCoroutine(Spawn(spawn));
    }

    private IEnumerator Spawn(ISpawn spawn)
    {
        yield return new WaitForSeconds(spawn.RespawnTime());
        spawn.Spawn();
    }
    
}
