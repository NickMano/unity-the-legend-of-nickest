using System;
using UnityEngine;

public class HealthController : MonoBehaviour
{

    [SerializeField, Min(0)] private int maxHealth;
    public int currentHealth { get; private set; }

    public event Action HealthChanged;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (amount <= 0) return;
        if (currentHealth == 0) return;

        if (currentHealth < amount)
        {
            currentHealth = 0;
            HealthChanged?.Invoke();
            return;
        }

        currentHealth -= amount;
        HealthChanged?.Invoke();
    }

    public int GetMaxHealth() => maxHealth;
}