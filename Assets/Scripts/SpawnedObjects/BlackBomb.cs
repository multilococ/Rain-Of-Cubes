using System;
using System.Collections;
using UnityEngine;

public class BlackBomb : SpawnPrefab
{
    private Exploder _exploder = new Exploder();

    public event Action<BlackBomb> OnDied;

    private void Start()
    {
        Init(transform.position);
    }

    public override void Init(Vector3 spawnPosition)
    {
        Renderer.material.color = Color.black;
        transform.position = spawnPosition;
        transform.rotation = Quaternion.identity;
        Rigidbody.velocity = Vector3.zero;
        StartCoroutine(DeathWithDelay(MinLifeTime, MaxLifeTime));
    }

    protected override IEnumerator DeathWithDelay(float minLifeTime, float maxLifeTime)
    {
        Color bombColor = Renderer.material.color;

        float lifeTime = UnityEngine.Random.Range(minLifeTime, maxLifeTime);
        float dealapsedTime = 0.0f;
        float priviosValue = bombColor.a;

        while (dealapsedTime < lifeTime)
        { 
            dealapsedTime += Time.deltaTime;

            float normalazedTime = dealapsedTime / lifeTime;
            float interminadateValue = Mathf.Lerp(priviosValue,0,normalazedTime);

            Renderer.material.color = new Color(bombColor.r,bombColor.g,bombColor.b,interminadateValue);
            
            yield return null;
        }
        
        _exploder.Explode(transform.position);
        OnDied?.Invoke(this);
    }
}
