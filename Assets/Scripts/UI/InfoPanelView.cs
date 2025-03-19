using TMPro;
using UnityEngine;

public abstract class InfoPanelView <T> : MonoBehaviour where T : SpawnPrefab
{
    [SerializeField] private Spawner<T> _spawner;

    [SerializeField] private TextMeshProUGUI _spawnedObjectsForAllTimeCounterText;
    [SerializeField] private TextMeshProUGUI _createdObjectsCounterText;
    [SerializeField] private TextMeshProUGUI _activeObjectsCounterText;

    private void OnEnable()
    {
        _spawner.SpawndeObjectForAllTimeChanged += SpawnedObjectsForAllTimeCounterChange;
        _spawner.CreatedObjectsCountChanged += CreatedObjectCounterChange;
        _spawner.ActiveObjectsCountChanged += ActiveObjectsCountChanged;
    }

    private void OnDisable()
    {
        _spawner.SpawndeObjectForAllTimeChanged -= SpawnedObjectsForAllTimeCounterChange;
        _spawner.CreatedObjectsCountChanged -= CreatedObjectCounterChange;
        _spawner.ActiveObjectsCountChanged -= ActiveObjectsCountChanged;
    }

    private void SpawnedObjectsForAllTimeCounterChange(int value) 
    {
        _spawnedObjectsForAllTimeCounterText.text = value.ToString();
    }
    
    private void CreatedObjectCounterChange(int value) 
    {
        _createdObjectsCounterText.text = value.ToString();
    }

    private void ActiveObjectsCountChanged(int value) 
    {
        _activeObjectsCounterText.text = value.ToString();
    }
}