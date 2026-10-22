using System;
using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField, Min(0)] private int maxHealth;
    public int currentHealth { get; private set; }

    public event Action HealthChanged;
    public event Action Died;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (amount <= 0 || currentHealth == 0) return;

        currentHealth = Mathf.Max(0, currentHealth - amount);
        HealthChanged?.Invoke();

        if (currentHealth == 0)
            Died?.Invoke();
    }

    public int GetMaxHealth() => maxHealth;
}