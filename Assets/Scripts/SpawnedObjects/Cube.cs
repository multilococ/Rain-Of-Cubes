using System;
using System.Collections;
using UnityEngine;

public class Cube : SpawnPrefab
{
    [SerializeField] private Color _defautlColor;

    private bool _isTouchedPlatform;

    public event Action<Cube> OnDied;

    public override void Init(Vector3 spawnPosition)
    {
        transform.position = spawnPosition;
        transform.rotation = Quaternion.identity;
        _isTouchedPlatform = false;
        Renderer.material.color = _defautlColor;
        Rigidbody.velocity = Vector3.zero; 
    }

    private void CollideWithPlatform()
    {
        _isTouchedPlatform = true;
        Renderer.material.color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Platform>() == null)
        {
            return;
        }

        if (_isTouchedPlatform == false)
        {
            CollideWithPlatform();
            StartCoroutine(DeathWithDelay(MinLifeTime, MaxLifeTime));
        }
    }

    protected override IEnumerator DeathWithDelay(float minLifeTime, float maxLifeTime)
    {
        float lifeTime = UnityEngine.Random.Range(minLifeTime, maxLifeTime);

        WaitForSeconds delay = new WaitForSeconds(lifeTime);

        yield return delay;

        OnDied?.Invoke(this);
    }
}
