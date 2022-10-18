using UnityEngine;

public interface ISpawn
{
    public void Spawn();
    public void DeSpawn();
    public float RespawnTime();

    public GameObject GetGameObject();
}
