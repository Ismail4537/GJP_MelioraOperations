using UnityEngine;
using System.Collections; 
using System.Collections.Generic;

public class BasatanAttack : MonoBehaviour, IAttackPattern 
{
    [SerializeField] private GameObject thornsPrefab;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private EnemyStatsSO stats;
    [SerializeField] private List<GameObject> enemies;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float defDistanceRay = 100;

    public Transform laserFirePoint;
    private Transform player;
    private Transform m_transform;

    public LineRenderer m_lineRenderer;
    private EnemyAttack enemyAttack;

    private EnemyMovement enemyMovement;

    private float timer;
    private float thornTimer;

    private float thornDuration = 5;
    private float laserDuration = 3f;

    private float timerSpawnEnemy;

    private float spawnEnemyCooldown = 10f;

    private bool shootLaser;

    private float rotatingOffset = 0f;

    [SerializeField] private int numberOfThorns = 8;

    private Transform parent;


    private void Start()
    {
        parent = transform.parent;

        player = GameObject.FindGameObjectWithTag("Player").transform;
        m_transform = transform;

        enemyAttack = transform.parent.Find("Scripts").GetComponent<EnemyAttack>();
        enemyMovement = transform.parent.Find("Scripts").GetComponent<EnemyMovement>();

        // laser straight = 2 titik
        m_lineRenderer.positionCount = 2;
        m_lineRenderer.enabled = false;
    }

    public void OnAttackStart()
    {
        timer = 0f;
        shootLaser = true;
        m_lineRenderer.enabled = true;
    }

    public void ExecuteAttack(EnemyController enemy)
    {
        if (shootLaser)
            enemyMovement.slowMove = true;
        else
            enemyMovement.slowMove = false;

            timer += Time.deltaTime;

        if (timer <= laserDuration)
        {
            shootLaser = true;
            m_lineRenderer.enabled = true;
            FireLaser();
        }
        else
        {
            if (timer >= stats.cooldown)
                timer = 0;

            shootLaser = false;
            m_lineRenderer.enabled = false;
        }

        thornTimer += Time.deltaTime;

        if (thornTimer >= thornDuration)
        {
            thornTimer = 0f;

            ShootThornsInAllDirections(enemy);
        }

        timerSpawnEnemy += Time.deltaTime;

        if (timerSpawnEnemy >= spawnEnemyCooldown)
        {
            timerSpawnEnemy = 0f;

            Transform t = enemy.transform;

            Vector3 topPos  = t.position - t.up * 2f;
            Vector3 bottomPos = t.position + t.up * 2f;

            if (enemies.Count > 0)
                Instantiate(enemies[0], topPos, Quaternion.identity);

            if (enemies.Count > 1)
                Instantiate(enemies[1], bottomPos, Quaternion.identity);
        }
    }

    private void ShootThornsInAllDirections(EnemyController enemy)
    {
        float angleStep = 360f / numberOfThorns;

        for (int i = 0; i < numberOfThorns; i++)
        {
            float angle = rotatingOffset + angleStep * i;
            float rad = angle * Mathf.Deg2Rad;

            Vector2 dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));

            var thorn = Instantiate(thornsPrefab, enemy.attackPoint.position, Quaternion.identity);
            thorn.GetComponent<Thorns>().Initialize(dir, enemy.GetComponentInChildren<EnemyAttack>(), stats);
        }

        rotatingOffset += 10f;
    }


    private void FireLaser()
    {
        if (!shootLaser)
            return;

        if (!m_lineRenderer.enabled)
            m_lineRenderer.enabled = true;

        RaycastHit2D hit = Physics2D.Raycast(
            laserFirePoint.position,
            m_transform.right,
            defDistanceRay,            
            playerLayer
        );

        if (hit.collider != null)
        {
            Draw2Dray(laserFirePoint.position, hit.point);

            if (hit.collider.CompareTag("Player"))
            {
                enemyAttack.Attack();
            }
        }
        else
        {
            // Laser tidak kena apa-apa, gambar sampai ujung
            Draw2Dray(laserFirePoint.position,
                laserFirePoint.position + laserFirePoint.right * defDistanceRay
            );
        }
    }
    
    private void Draw2Dray(Vector2 startPos, Vector2 endPos)
    {
        m_lineRenderer.SetPosition(0, startPos);
        m_lineRenderer.SetPosition(1, endPos);
    }
}
