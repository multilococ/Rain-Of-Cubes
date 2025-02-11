using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;

    [SerializeField] private float _yPosition;

    [SerializeField] private float _repiatingDelay = 1;

    private ObjectPool<GameObject> _pool;

    private int _poolCapacity = 10;
    private int _poolMaxSize = 10;

    private void Awake()
    {
        _pool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(_prefab),
            actionOnRelease: (obj) => obj.SetActive(false),
            actionOnGet: (obj) => ActionOnGet(obj),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    private void Start()
    {
        InvokeRepeating(nameof(GetCube), 0.0f, _repiatingDelay);
    }

    public void ReleaseCube(GameObject cube)
    {
        _pool.Release(cube);
    }

    private void GetCube()
    {
        GameObject cube = _pool.Get();
        cube.GetComponent<Cube>().InitCube();
    }

    private void ActionOnGet(GameObject cube)
    {
        cube.transform.position = GetRandomPosition();
        cube.GetComponent<Rigidbody>().velocity = Vector3.zero;
        cube.transform.rotation = Quaternion.Euler(Vector3.zero);
        cube.SetActive(true);
    }

    private Vector3 GetRandomPosition()
    {
        float MaxPositionX = 4.5f;
        float MinPositionX = -4.5f;
        float MaxPositionZ = 4.5f;
        float MinPositionZ = -4.5f;

        return new Vector3(Random.Range(MinPositionX, MaxPositionX), _yPosition, Random.Range(MinPositionZ, MaxPositionZ));
    }
}
