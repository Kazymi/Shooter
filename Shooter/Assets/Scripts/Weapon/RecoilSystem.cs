using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(ShotSystem), typeof(WeaponStateMachine))]
public class RecoilSystem : MonoBehaviour
{
    [SerializeField] private float xRecoil;
    [SerializeField] private float yRecoil;
    [SerializeField] private Transform recoilCamera;

    private bool IsShooted;
    private float returnValue;
    private float shotTime;

    private WeaponStateMachine _weaponStateMachine;
    private Vector3 recoilCurrent;


    private void Awake()
    {
        _weaponStateMachine = GetComponent<WeaponStateMachine>();
        GetComponent<ShotSystem>().Shooted += AddRecoil;
    }


    private void AddRecoil()
    {
        shotTime = _weaponStateMachine.FireRate + 0.05f;
        returnValue = 0.1f;
        IsShooted = true;
    }


    private void Update()
    {
        shotTime -= Time.deltaTime;
        if (shotTime < 0)
        {
            returnValue = 100;
        }
    }

    private void LateUpdate()
    {
        if (IsShooted)
        {
            recoilCurrent -= new Vector3(xRecoil, Random.Range(0, 2) == 1 ? -yRecoil : yRecoil, 0);
            recoilCurrent.y = Mathf.Clamp(recoilCurrent.y, -1, 1);
            recoilCamera.transform.DOLocalRotate(recoilCurrent, 0);
        }

        IsShooted = false;
    }
}