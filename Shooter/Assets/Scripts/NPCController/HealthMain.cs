using System;
using UnityEngine;

public abstract class HealthMain : MonoBehaviour, IDamageable
{
    [SerializeField] protected int maxHealth;

    protected int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
        OnAwake();
    }

    protected virtual void OnAwake()
    {
    }

    public virtual void TakeDamage(int damage, DamageType damageType = DamageType.Player)
    {
        if (damageType == DamageType.Player)
        {
            currentHealth -= damage;
            HealthUpdated();
        }
    }

    protected virtual void HealthUpdated()
    {
    }
}