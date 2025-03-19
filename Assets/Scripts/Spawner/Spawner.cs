using System;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner <T> : MonoBehaviour where T : SpawnPrefab
{
    [SerializeField] private T _prefab;

    private int _poolCapacity = 10;
    private int _poolMaxSize = 10;
    private int _spawnedObjectForAllTimeCount = 0;
    private int _createdObjectsCount = 0;

    protected int _activeObjectsCount = 0;

    protected ObjectPool<T> _pool;

    public event Action<int> SpawndeObjectForAllTimeChanged;
    public event Action<int> CreatedObjectsCountChanged;
    public event Action<int> ActiveObjectsCountChanged;

    private void GetObjectFromPool(T prefab)
    {
        ChangeSpawnedObjectsCounter();
        prefab.gameObject.SetActive(true);
    }

    private T CreatePrefab()
    {
        ChangeCreatedObjectsCounter();

        return Instantiate(_prefab);
    }

    private void ChangeSpawnedObjectsCounter()
    {
        _spawnedObjectForAllTimeCount++;
        SpawndeObjectForAllTimeChanged?.Invoke(_spawnedObjectForAllTimeCount);
    }
 
    private void ChangeCreatedObjectsCounter()
    {
        _createdObjectsCount++;
        CreatedObjectsCountChanged?.Invoke(_createdObjectsCount);
    }

    protected ObjectPool<T> CreatePool() 
    {
        return new ObjectPool<T>(
                createFunc: CreatePrefab,
                actionOnRelease: (prefab) => prefab.gameObject.SetActive(false),
                actionOnGet: (prefab) => GetObjectFromPool(prefab),
                actionOnDestroy: (prefab) => Destroy(prefab),
                collectionCheck: true,
                defaultCapacity: _poolCapacity,
                maxSize: _poolMaxSize) ;
    }

    protected void IncreaseActiveObjectsCounter() 
    {
        _activeObjectsCount++; 
        ActiveObjectsCountChanged?.Invoke(_activeObjectsCount);
    }  
    
    protected void ReduceActiveObjectsCounter() 
    {
        _activeObjectsCount--; 
        ActiveObjectsCountChanged?.Invoke(_activeObjectsCount);
    }

    protected abstract void ReleasePrefab(T prefab);

    protected abstract void GetPrefab(Vector3 spawnPosition);
}
