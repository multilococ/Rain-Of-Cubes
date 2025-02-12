using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private Color _defautlColor;

    private Renderer _renderer;

    private Rigidbody _rigidbody;

    private float _minLifeTime = 2f;
    private float _maxLifeTime = 5f;

    private bool _isTouchedPlatform;

    public event Action<Cube> OnDied;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
        Init();
    }

    public void Init()
    {
        _isTouchedPlatform = false;
        _renderer.material.color = _defautlColor;
        _rigidbody.velocity = Vector3.zero; 
    }

    private void CollideWithPlatform()
    {
        _isTouchedPlatform = true;
        _renderer.material.color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
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
            StartCoroutine(DeathWithDelay(_minLifeTime, _maxLifeTime));
        }
    }

    private IEnumerator DeathWithDelay(float minLifeTime, float maxLifeTime)
    {
        float lifeTime = UnityEngine.Random.Range(minLifeTime, maxLifeTime);

        WaitForSeconds delay = new WaitForSeconds(lifeTime);

        yield return delay;

        OnDied?.Invoke(this);
    }
}
