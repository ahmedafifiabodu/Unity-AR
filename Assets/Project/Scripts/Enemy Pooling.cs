using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPooling : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
        public LayerMask m_Player;
        public Transform parentTransform;
        public float health;
        public int damage;
        public float speed;
        public float detectionRadius;
        public float stoppingDistance;
    }

    [SerializeField] private List<Pool> pools;
    [SerializeField] private Transform player;
    [SerializeField] private float spawnRadius = 5f;
    [SerializeField] private float spawnRate = 1f;

    private Dictionary<string, Queue<GameObject>> poolDictionary;

    public static EnemyPooling Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void Start()
    {
        poolDictionary = new();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, pool.parentTransform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }

        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            foreach (Pool pool in pools)
            {
                if (CountActiveEnemies(pool.tag) < pool.size)
                {
                    Vector3 spawnPosition = player.position + new Vector3(Random.insideUnitCircle.x, 0f, Random.insideUnitCircle.y) * spawnRadius;
                    spawnPosition.y = 0;
                    SpawnFromPool(pool.tag, spawnPosition, Quaternion.identity);
                }
            }
            yield return new WaitForSeconds(1f / spawnRate);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.SetPositionAndRotation(position, rotation);

        foreach (Pool pool in pools)
        {
            if (pool.tag == tag)
            {
                objectToSpawn.transform.SetParent(pool.parentTransform);
                if (objectToSpawn.TryGetComponent<EnemyMovement>(out var enemyMovement))
                {
                    enemyMovement.health = pool.health;
                    enemyMovement.damage = pool.damage;
                    enemyMovement.speed = pool.speed;
                    enemyMovement.m_Player = pool.m_Player;
                    enemyMovement.stoppingDistance = pool.stoppingDistance;
                    enemyMovement.detectionRadius = pool.detectionRadius;
                }

                break;
            }
        }

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    private int CountActiveEnemies(string tag)
    {
        int activeEnemies = 0;
        foreach (var enemy in poolDictionary[tag])
        {
            if (enemy.activeInHierarchy)
            {
                activeEnemies++;
            }
        }
        return activeEnemies;
    }

    public void ResetEnemies()
    {
        foreach (var pool in pools)
        {
            foreach (var enemy in poolDictionary[pool.tag])
            {
                if (enemy.activeInHierarchy)
                {
                    enemy.SetActive(false);
                    if (enemy.TryGetComponent<EnemyMovement>(out var enemyMovement))
                    {
                        enemyMovement.health = pool.health;
                        enemyMovement.damage = pool.damage;
                        enemyMovement.speed = pool.speed;
                        enemyMovement.m_Player = pool.m_Player;
                        enemyMovement.stoppingDistance = pool.stoppingDistance;
                        enemyMovement.detectionRadius = pool.detectionRadius;
                    }
                }
            }
        }
    }
}