using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _prefab;

    [SerializeField] private SpawnArea _spawnArea;

    [SerializeField] private float _repiatingDelay = 1;

    private ObjectPool<Cube> _pool;

    private int _spawnCount = 100;
    private int _poolCapacity = 10;
    private int _poolMaxSize = 10;

    private void Awake()
    {
        _pool = InitPool();
        StartCoroutine(SpawnCubes(_spawnCount, _repiatingDelay));
    }

    private ObjectPool<Cube> InitPool() 
    {
        return new ObjectPool<Cube>(
            createFunc: () => Instantiate(_prefab),
            actionOnRelease: (cube) => cube.gameObject.SetActive(false),
            actionOnGet: (cube) => GetObjectFromPool(cube),
            actionOnDestroy: (cube) => Destroy(cube),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    public void ReleaseCube(Cube cube)
    {
        cube.OnDeath -= ReleaseCube;
        _pool.Release(cube);
    }

    private void GetObjectFromPool(Cube cube)
    {
        cube.transform.position = _spawnArea.GetRandomPosition();
        cube.GetComponent<Rigidbody>().velocity = Vector3.zero;
        cube.transform.rotation = Quaternion.Euler(Vector3.zero);
        cube.gameObject.SetActive(true);
    }

    private void GetCube()
    {
        Cube cube = _pool.Get();
        cube.Init();
        cube.OnDeath += ReleaseCube;
    }

    private IEnumerator SpawnCubes(int spawnCount, float delay)
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(delay);

        for (int i = 0; i < spawnCount; i++)
        {
            GetCube();

            yield return waitForSeconds;
        }
    }
}
