using UnityEngine;

public class LaserV2 : MonoBehaviour {
    public float speed = 12f;
    public float turnInterval = 0.15f; // berapa sering laser "snap" ke player
    public float maxLifetime = 5f;

    private Transform player;
    private Vector2 direction;
    private float timer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // awalnya maju lurus sesuai rotasi prefab
        direction = transform.right;

        Destroy(gameObject, maxLifetime);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= turnInterval)
        {
            timer = 0f;
            SnapTurnToPlayer();
        }

        // maju ke arah yang sudah diset
        transform.position += (Vector3)direction * speed * Time.deltaTime;
    }

    void SnapTurnToPlayer()
    {
        if (!player) return;

        Vector2 toPlayer = (player.position - transform.position).normalized;

        // GANTI ARAH LANGSUNG MENGHADAP PLAYER
        direction = toPlayer;

        // update rotasi sprite / laser
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // damage di sini kalau perlu
            // other.GetComponent<PlayerHealth>().TakeDamage(1);

            Destroy(gameObject);
        }
    }
}