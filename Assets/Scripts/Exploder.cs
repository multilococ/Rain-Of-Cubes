using UnityEngine;

public class Exploder
{
    private float _explosionRange = 50f;
    private float _explosionForce = 750f;

    public void Explode(Vector3 explodePoint)
    {
        Collider[] overlappdeColliders = Physics.OverlapSphere(explodePoint, _explosionRange);

        foreach (Collider collider in overlappdeColliders)
        {
            Rigidbody rigidbody = collider.attachedRigidbody;

            if (rigidbody != null)
            {
                rigidbody.AddExplosionForce(_explosionForce, explodePoint, _explosionRange);
            }
        }
    }
}
