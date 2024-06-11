using UnityEngine;

[RequireComponent(typeof(ShotSystem))]
public class Reloader : MonoBehaviour
{
    [SerializeField] private int amountMax;

    private float currentAmount;

    private void Awake()
    {
        Reload();
        GetComponent<ShotSystem>().Shooted += ReduceBullet;
    }

    public void Reload()
    {
        currentAmount = amountMax;
    }

    public void ReduceBullet()
    {
        currentAmount--;
    }

    public bool IsMagFull()
    {
        return currentAmount == amountMax;
    }
    public bool CheckAmount()
    {
        return currentAmount > 0;
    }
}