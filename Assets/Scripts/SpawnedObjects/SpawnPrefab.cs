using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
public abstract class SpawnPrefab : MonoBehaviour
{
    protected Renderer Renderer;
    protected Rigidbody Rigidbody;

    protected float MinLifeTime = 2f;
    protected float MaxLifeTime = 5f;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Renderer = GetComponent<Renderer>();
    }

    public abstract void Init(Vector3 spawnPosition);

    protected abstract IEnumerator DeathWithDelay(float minLifetTime, float maxLifeTime);
}