using DG.Tweening;
using UnityEngine;

public class CustomizationController : MonoBehaviour
{
    [SerializeField] private Color aliveColor;
    [SerializeField] private Color deadColor;
    [SerializeField] private NPCHealthController healthController;
    [SerializeField] private GameObject[] bodyRenderers;
    [SerializeField] private GameObject[] hatRenderers;
    [SerializeField] private WeaponConfiguration[] weaponRenderers;

    private SkinnedMeshRenderer currentSkinMesh;

    private void OnNPCDead()
    {
        currentSkinMesh.material.DOColor(deadColor, "_Color", 1);
    }

    private void Awake()
    {
        healthController.OnHealthEmpty += OnNPCDead;
    }

    private void OnEnable()
    {
        foreach (var bodyRenderer in bodyRenderers)
        {
            bodyRenderer.SetActive(false);
        }

        var id = Random.Range(0, bodyRenderers.Length - 1);
        currentSkinMesh = bodyRenderers[id].GetComponent<SkinnedMeshRenderer>();
        bodyRenderers[id].SetActive(true);
        currentSkinMesh.material.DOColor(aliveColor, "_Color", 0);

        foreach (var hatRenderer in hatRenderers)
        {
            hatRenderer.SetActive(false);
        }

        hatRenderers[Random.Range(0, hatRenderers.Length - 1)].SetActive(true);

        foreach (var weapon in weaponRenderers)
        {
            weapon.BodyWeapon.SetActive(false);
            weapon.HandWeapon.SetActive(false);
        }

        var foundWeaponRender = weaponRenderers[Random.Range(0, weaponRenderers.Length - 1)];
        foundWeaponRender.BodyWeapon.SetActive(true);
        foundWeaponRender.HandWeapon.SetActive(true);
    }
}