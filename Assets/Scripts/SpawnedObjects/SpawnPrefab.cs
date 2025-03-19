using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
public abstract class SpawnPrefab : MonoBehaviour
{
    protected Renderer _renderer;
    protected Rigidbody _rigidbody;

    protected float _minLifeTime = 2f;
    protected float _maxLifeTime = 5f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
    }

    public abstract void Init(Vector3 spawnPosition);

    protected abstract IEnumerator DeathWithDelay(float minLifetTime, float maxLifeTime);
}