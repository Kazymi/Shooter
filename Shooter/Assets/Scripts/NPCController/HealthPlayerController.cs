using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthPlayerController : HealthMain
{
    [SerializeField] private Image image;
    private bool isAlive = true;

    public override void TakeDamage(int damage, DamageType damageType = DamageType.Player)
    {
        if (damageType == DamageType.Enemy)
        {
            currentHealth -= damage;
            HealthUpdated();
        }
    }

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
            GetComponent<FirstPersonMovement>().enabled = false;
            GetComponentInChildren<FirstPersonLook>().enabled = false;
            GetComponentInChildren<WeaponStateMachine>().enabled = false;
            image.DOFade(1, 0.7f).OnComplete(() => SceneManager.LoadScene(0));
        }
    }
}