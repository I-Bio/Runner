using UnityEngine;

public class HealthBar : ObjectPool
{
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private GameObject _heartTemplate;

    private int _currentHeartsCount;
    
    private void Awake()
    {
        Preparing();
    }
    
    private void OnEnable()
    {
        _playerHealth.HealthChanged += OnChangedHealth;
    }

    private void OnDisable()
    {
        _playerHealth.HealthChanged -= OnChangedHealth;
    }

    private void Preparing()
    {
        for (int i = 0; i < _playerHealth.MaxHealth; i++)
        {
            Initialize(_heartTemplate);
        }
    }
    
    private void OnChangedHealth(int value)
    {
        int currentHeartsCount = _currentHeartsCount;
        
        if (currentHeartsCount < value)
        {
            for (int i = 0; i < value - currentHeartsCount; i++)
            {
                CreateHeart();
            }
        }
        else if (currentHeartsCount > value)
        {
            for (int i = 0; i < currentHeartsCount - value; i++)
            {
                DeactivateHeart(currentHeartsCount - 1);
            }
        }
    }

    private void CreateHeart()
    {
        if (TryGetFirstObjectComponent(out Heart heart) == true)
        {
            _currentHeartsCount++;
            heart.ToFill();
        }
    }

    private void DeactivateHeart(int id)
    {
        if (TryGetObjectComponent(out Heart heart, id) == true)
        {
            _currentHeartsCount--;
            heart.ToEmpty();
        }
    }
}
