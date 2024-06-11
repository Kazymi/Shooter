using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Granade : MonoPooled
{
    [SerializeField] private LayerMask _mask;
    [SerializeField] private float explosionRadius;
    [SerializeField] private float force;
    [SerializeField] private float Explosionforce;
    [SerializeField] private float distanceScaleFactor;

    private Rigidbody _rigidbody;
    private EffectSpawner _effectSpawner;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }


    public void AddVelocity()
    {
        transform.parent = null;
        _rigidbody.velocity = Camera.main.transform.forward * force;
        StartCoroutine(Explosion());
    }

    private IEnumerator Explosion()
    {
        yield return new WaitForSeconds(4);
        var foundObject = Physics.OverlapSphere(transform.position, explosionRadius, _mask)
            .Where(t => t.GetComponent<Rigidbody>());
        var foundEnemy = Physics.OverlapSphere(transform.position, explosionRadius, _mask)
            .Where(t => t.GetComponent<NPCHealthController>()).ToList();
        foreach (var enemy in foundEnemy)
        {
            var scale = Vector3.Distance(enemy.transform.position, transform.position);
            var newDamage =(int) (50-(distanceScaleFactor * scale * 50));
            enemy.GetComponent<NPCHealthController>().TakeDamage(newDamage);
        }

        _effectSpawner ??= ServiceLocator.GetService<EffectSpawner>();

        _effectSpawner.SpawnEffect(transform.position, EffectType.ExplosionGrenade);
        foreach (var rbObject in foundObject)
        {
            rbObject.GetComponent<Rigidbody>().AddExplosionForce(Explosionforce, transform.position, explosionRadius, 2,
                ForceMode.Impulse);
        }

        ReturnToPool();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}