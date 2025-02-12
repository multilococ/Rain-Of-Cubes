using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    public UnityAction<Cube> OnDeath;

    [SerializeField] private Color _defautlColor;

    private Renderer _renderer;

    private float _minLifeTime = 2f;
    private float _maxLifeTime = 5f;

    private bool _isTouchedPlatform;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        Init();
    }

    public void Init()
    {
        _isTouchedPlatform = false;
        _renderer.material.color = _defautlColor;
    }

    private void CollideWithPlatform()
    {
        _isTouchedPlatform = true;
        _renderer.material.color = new Color(Random.value, Random.value, Random.value);
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
        float lifeTime = Random.Range(minLifeTime, maxLifeTime);

        WaitForSeconds delay = new WaitForSeconds(lifeTime);

        yield return delay;

        OnDeath?.Invoke(this);
    }
}
