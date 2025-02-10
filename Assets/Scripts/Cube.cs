using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    private Renderer _renderer;
    private Color _defautlColor;

    private bool _isColorChanged;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        _isColorChanged = false;
         _renderer.material.color = Color.green;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<Platform>() is Platform) 
        {
            if (_isColorChanged == false)
            {
                SetRandomColor();
                _isColorChanged = true;
            }
        }
    }

    private void SetRandomColor() 
    {    
        _renderer.material.color = new Color(Random.value, Random.value, Random.value);
    }
}
