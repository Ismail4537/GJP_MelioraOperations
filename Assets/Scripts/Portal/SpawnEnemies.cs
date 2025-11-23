using UnityEngine;
using System.Collections; // Butuh ini untuk IEnumerator
using System.Collections.Generic;
using System.Linq;

public class SpawnEnemies : MonoBehaviour
{
    [Header("Global Enemy Pool")]
    [SerializeField] private List<GameObject> allEnemies; 
    
    [Header("Spawn Point")]
    [SerializeField] private Transform spawnPoint;
    
    [SerializeField] private float spawnRadius = 5f; 
    
    [SerializeField] private float minSpawnRadius = 3f;

    [Header("Jump Settings")]
    [Range(0f, 1f)] [SerializeField] private float jumpChance = 0.5f; 
    [SerializeField] private float jumpDuration = 0.8f; 
    [SerializeField] private float jumpHeight = 2f; 
    [Header("Wave Configuration")]
    [SerializeField] private List<WavesSO> waves;

    private int currentWaveIndex = 0;
    private float waveTimer;
    private float spawnTimer;
    
    private bool isWaveActive = true;
    private GameObject activeBoss;
    private List<GameObject> currentWaveEnemies;


    private void Start()
    {
        StartWave();
    }

    private void Update()
    {
        if (!isWaveActive) return;

        // Logic Boss
        if (waves[currentWaveIndex].isBossWave)
        {
            if (activeBoss == null)
            {
                Debug.Log("Boss Defeated! Moving to next wave.");
                NextWave();
            }
            return;
        }

        // Logic Wave Biasa
        waveTimer += Time.deltaTime;
        if (waveTimer >= waves[currentWaveIndex].durationInSeconds)
        {
            NextWave();
            return;
        }

        spawnTimer += Time.deltaTime;
        if (spawnTimer >= waves[currentWaveIndex].spawnInterval)
        {   
            spawnTimer = 0f;
            SpawnRandomEnemy();
        }
    }

    private void StartWave()
    {
        WavesSO currentData = waves[currentWaveIndex];
        
        waveTimer = 0f;
        spawnTimer = 0f;
        isWaveActive = true;

        if (currentData.isBossWave)
            SpawnBoss(currentData.bossPrefab);
        else
            FilterEnemiesForThisWave(currentData.categoriesToSpawn);
    }

    private void NextWave()
    {
        currentWaveIndex++;
        if (currentWaveIndex >= waves.Count)
        {
            isWaveActive = false;
            return;
        }
        StartWave();
    }

    private void FilterEnemiesForThisWave(List<string> allowedCategories)
    {
        currentWaveEnemies = new List<GameObject>();
        foreach (GameObject enemyPrefab in allEnemies)
        {
            EnemyStatsRef statsRef = enemyPrefab.GetComponentInChildren<EnemyStatsRef>();
            if (statsRef != null && allowedCategories.Contains(statsRef.stats.enemyCategory))
            {
                currentWaveEnemies.Add(enemyPrefab);
            }
        }
    }

    private void SpawnBoss(GameObject bossPrefab)
    {
        if (bossPrefab == null) return;
        activeBoss = Instantiate(bossPrefab, spawnPoint.position, Quaternion.identity);
    }


    private void SpawnRandomEnemy()
    {
        if (currentWaveEnemies.Count == 0) return;

        int rng = Random.Range(0, currentWaveEnemies.Count);
        GameObject selectedEnemyPrefab = currentWaveEnemies[rng];

        Vector2 randomDirection = Random.insideUnitCircle.normalized;

        float randomDistance = Random.Range(minSpawnRadius, spawnRadius);

        Vector2 targetPos = (Vector2)spawnPoint.position + (randomDirection * randomDistance);


        bool shouldJump = Random.value < jumpChance; 

        if (shouldJump)
        {
            GameObject enemy = Instantiate(selectedEnemyPrefab, spawnPoint.position, Quaternion.identity);
            StartCoroutine(JumpRoutine(enemy, targetPos));
        }
        else
        {
            Instantiate(selectedEnemyPrefab, targetPos, Quaternion.identity);
        }
    }

    private IEnumerator JumpRoutine(GameObject enemy, Vector2 destination)
    {
        var movementScript = enemy.GetComponent<MonoBehaviour>(); 
        var col = enemy.GetComponent<Collider2D>();

        if (movementScript != null) movementScript.enabled = false;
        if (col != null) col.enabled = false;

        Vector2 startPos = enemy.transform.position;
        float timer = 0f;

        while (timer < jumpDuration)
        {
            
            if (enemy == null) yield break;

            timer += Time.deltaTime;
            float progress = timer / jumpDuration; 

            Vector2 linearPos = Vector2.Lerp(startPos, destination, progress);

            float heightCurve = 4 * jumpHeight * progress * (1 - progress);

            enemy.transform.position = new Vector2(linearPos.x, linearPos.y + heightCurve);

            yield return null; 
        }

        // 2. Mendarat
        if (enemy != null)
        {
            enemy.transform.position = destination; 
            if (movementScript != null) movementScript.enabled = true;
            if (col != null) col.enabled = true;
        }
    }
    
    private void OnDrawGizmos()
    {
        if (spawnPoint != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(spawnPoint.position, spawnRadius);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(spawnPoint.position, minSpawnRadius);
        }
    }
}