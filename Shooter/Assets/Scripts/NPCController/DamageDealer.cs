using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private int damage;

    private HealthPlayerController target;

    private void Awake()
    {
        target = FindObjectOfType<HealthPlayerController>();
    }

    public void TakeDamageToPlayer()
    {
        if (Vector3.Distance(transform.position, target.transform.position) < 2.6f)
        {
            target.TakeDamage(damage, DamageType.Enemy);
        }
    }
}