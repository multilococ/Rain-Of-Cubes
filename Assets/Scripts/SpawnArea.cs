using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    [SerializeField] private float _xRange = 4.5f;
    [SerializeField] private float _zRange = 4.5f;

    public Vector3 GetRandomPosition() 
    {
        float yPosition = transform.position.y;
        float xPosition = transform.position.x;
        float zPosition = transform.position.z;
        float maxPositionX = xPosition + _xRange;
        float minPositionX = xPosition - _xRange;
        float maxPositionZ = zPosition + _zRange;
        float minPositionZ = zPosition - _zRange;

        return new Vector3(Random.Range(minPositionX,maxPositionX),yPosition,Random.Range(minPositionZ,maxPositionZ));
    }
}
