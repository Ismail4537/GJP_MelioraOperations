using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2;
    [SerializeField] private Transform player;

    private Transform spriteObject;

    private Rigidbody2D rb;

    private Transform parent;

    private void Start()
    {
        parent = transform.parent;

        rb = parent.GetComponent<Rigidbody2D>();

        spriteObject = parent.Find("Sprite");
    }

    private void Update()
    {
        Chase();
        // Flip();
    }

    void Chase()
    {
        Vector2 direction = (player.position - parent.position).normalized;
        rb.linearVelocity = direction * speed;

        // rotasi musuh agar lihat ke player (top-down)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        parent.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Flip()
    {
        if (player.position.x > parent.position.x)
            spriteObject.localScale = new Vector3(1, 1, 1);
        else
            spriteObject.localScale = new Vector3(-1, 1, 1);
    }

    private void OnDrawGizmos()
    {
        if (player == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, player.position);
    }
}
