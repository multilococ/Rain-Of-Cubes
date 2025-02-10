using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private float _yPosition;

    private void Start()
    {
        Instantiate(_prefab, GenerateRandomPosition(), Quaternion.identity);
    }

    private Vector3 GenerateRandomPosition()
    {
        float MaxPositionX = 5;
        float MinPositionX = -5;
        float MaxPositionZ = 5;
        float MinPositionZ = -5;

        return new Vector3(Random.RandomRange(MinPositionX, MaxPositionX), _yPosition, Random.RandomRange(MinPositionZ, MaxPositionZ));
    }
}
