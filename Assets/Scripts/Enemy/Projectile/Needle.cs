using UnityEngine;

public class Needle : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    [SerializeField] private float lifeTime = 5f;

    private Vector2 direction;
    private EnemyAttack enemyAttack;

    public void Initialize(Vector2 dir, EnemyAttack attacker)
    {
        direction = dir.normalized;
        enemyAttack = attacker; 
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
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