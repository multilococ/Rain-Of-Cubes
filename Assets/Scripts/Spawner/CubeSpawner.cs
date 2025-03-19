using System;
using System.Collections;
using UnityEngine;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private SpawnArea _spawnArea;

    [SerializeField] private float _repiatingDelay = 1;
    
    private int _maxSpawnCount = 100;

    public event Action<Vector3> CubeRealesed;

    private void Awake()
    {
        _pool = CreatePool();
        StartCoroutine(SpawnCubes(_maxSpawnCount,_repiatingDelay));
    }

    protected override void GetPrefab(Vector3 spawnPosition)
    {
        Cube cube = _pool.Get();
        cube.Init(spawnPosition);
        cube.OnDied += ReleasePrefab;
        IncreaseActiveObjectsCounter();
    }

    protected override void ReleasePrefab(Cube prefab)
    {
        prefab.OnDied -= ReleasePrefab;
        CubeRealesed?.Invoke(prefab.transform.position);
        _pool.Release(prefab);
        ReduceActiveObjectsCounter();
    }

    private IEnumerator SpawnCubes(int spawnCount, float delay)
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(delay);

        for (int i = 0; i < spawnCount; i++)
        {
            GetPrefab(_spawnArea.GetRandomPosition());

            yield return waitForSeconds;
        }
    }
}
