using System;
using Unity.VisualScripting;
using UnityEngine;

public class NPCHealthController : HealthMain
{
    [SerializeField] private RagdollController ragdollController;

    public event Action OnHealthEmpty;
    private bool isAlive = true;

    private void OnEnable()
    {
        currentHealth = maxHealth;
    }

    protected override void HealthUpdated()
    {
        if (isAlive == false)
        {
            return;
        }

        if (currentHealth <= 0)
        {
            isAlive = false;
            OnHealthEmpty?.Invoke();
            ragdollController.SetRagDoll(true);
        }
    }
}