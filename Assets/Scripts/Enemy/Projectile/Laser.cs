using UnityEngine;

public class Laser : MonoBehaviour
{
    private Vector2 direction;
    private float speed = 8f;
    private bool homing;

    private Transform player;

    private EnemyAttack enemyAttack;

    public void Initialize(Vector2 dir, EnemyAttack attacker, bool isHoming)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyAttack = attacker;
        direction = dir.normalized;
        homing = isHoming;
    }

    private void Update()
    {
        if (homing && player != null)
        {
            Vector2 targetDir = (player.position - transform.position).normalized;

            float turnSpeed = 3f;

            direction = Vector2.Lerp(direction, targetDir, Time.deltaTime * turnSpeed).normalized;
        }

        transform.position += (Vector3)direction * speed * Time.deltaTime;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enemyAttack.Attack(); 
            Destroy(gameObject);
        }
    }
}
