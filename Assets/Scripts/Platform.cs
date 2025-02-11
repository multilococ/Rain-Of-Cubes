using System.Collections;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;

    private float _minCubeLifeTime = 2f;
    private float _maxCubeLifeTime = 5f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Cube cube))
        {
            if (cube.IsTouchedPlatfome == false)
            {
                float delay = Random.Range(_minCubeLifeTime, _maxCubeLifeTime);

                cube.CollisionWithPlatform();
                StartCoroutine(ReleseCubeWithDelay(cube,delay));
            }
        }
    }

    private IEnumerator ReleseCubeWithDelay(Cube cube,float delay) 
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(delay);

        yield return waitForSeconds;

        _spawner.ReleaseCube(cube.gameObject);
    }
}