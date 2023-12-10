using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth;

    private int _currentHealth;

    public event Action<int> HealthChanged;
    public event Action Died;
    public int MaxHealth => _maxHealth;
    
    private void Start()
    {
        Init();
    }

    public void TakeDamage()
    {
        _currentHealth--;

        if (_currentHealth == 0)
            Died?.Invoke();
        
        HealthChanged?.Invoke(_currentHealth);
    }

    public void Heal()
    {
        if (_currentHealth + 1 > _maxHealth)
            return;
        
        _currentHealth++;
        HealthChanged?.Invoke(_currentHealth);
    }

    private void Init()
    {
        _currentHealth = _maxHealth;
        HealthChanged?.Invoke(_currentHealth);
    }
}
