using System;
using UnityEngine;

public class ShotSystem : MonoBehaviour
{
    [SerializeField] private Transform shotCamera;
    [SerializeField] private BulletType bulletType;

    private BulletSpawner bulletSpawner;
    public event Action Shooted;

    public void Shot()
    {
        Shooted?.Invoke();
        bulletSpawner ??= ServiceLocator.GetService<BulletSpawner>();
        bulletSpawner.SpawnBullet(shotCamera.transform, bulletType);
    }
}