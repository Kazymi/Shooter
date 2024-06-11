using System.Collections;
using UnityEngine;

public class CharacterNPSSpawner : MonoBehaviour
{
    [SerializeField] private NPCStateMachine prefab;
    [SerializeField] private NPCSpawnConfiguration[] npcSpawnConfigurations;
    [SerializeField] private float interval;

    [SerializeField] private int amountSpawnInOneTime;

    private IPool<NPCStateMachine> npcPool;

    private void Awake()
    {
        var factory = new FactoryMonoObject<NPCStateMachine>(prefab.gameObject, transform);
        npcPool = new Pool<NPCStateMachine>(factory, 5);
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            for (int i = 0; i < amountSpawnInOneTime; i++)
            {
                var spawnConfig = npcSpawnConfigurations[Random.Range(0, npcSpawnConfigurations.Length - 1)];
                var x = Random.Range(-spawnConfig.SpawnScale.x / 2, spawnConfig.SpawnScale.x / 2) +
                        spawnConfig.SpawnCenter.position.x;
                var y = Random.Range(-spawnConfig.SpawnScale.y / 2, spawnConfig.SpawnScale.y / 2) +
                        spawnConfig.SpawnCenter.position.z;
                var randomPosition = new Vector3(x, spawnConfig.SpawnCenter.transform.position.y, y);
                npcPool.Pull().transform.position = randomPosition;
            }
        }
    }

    private void OnDrawGizmos()
    {
        foreach (var npc in npcSpawnConfigurations)
        {
            if (npc.SpawnCenter != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawCube(npc.SpawnCenter.position, new Vector3(npc.SpawnScale.x, 0.5f, npc.SpawnScale.y));
            }
        }
    }
}