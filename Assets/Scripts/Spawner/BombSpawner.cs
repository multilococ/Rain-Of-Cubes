using UnityEngine;

public class BombSpawner : Spawner<BlackBomb>
{
    [SerializeField] private CubeSpawner _cubeSpawner;

    private void Awake()
    {
        _pool = CreatePool();
    }

    private void OnEnable()
    {
        _cubeSpawner.CubeRealesed += GetPrefab;
    }

    private void OnDisable()
    {
        _cubeSpawner.CubeRealesed -= GetPrefab;
    }

    protected override void GetPrefab(Vector3 spawnPosition)
    {
        BlackBomb blackBomb = _pool.Get();
        blackBomb.Init(spawnPosition);
        blackBomb.OnDied += ReleasePrefab;
        IncreaseActiveObjectsCounter();
    }

    protected override void ReleasePrefab(BlackBomb prefab)
    {
        prefab.OnDied -= ReleasePrefab;
        _pool.Release(prefab);
        ReduceActiveObjectsCounter();
    }
}