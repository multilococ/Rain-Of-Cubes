using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    [SerializeField] private Color _defautlColor;

    private Renderer _renderer;

    private bool _isTouchedPlatfome;

    public bool IsTouchedPlatfome => _isTouchedPlatfome;


    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        InitCube();
    }

    public void InitCube()
    {
        _isTouchedPlatfome = false;
        _renderer.material.color = _defautlColor;
    }

    public void CollisionWithPlatform()
    {
        _isTouchedPlatfome = true;
        _renderer.material.color = new Color(Random.value, Random.value, Random.value);
    }
}
