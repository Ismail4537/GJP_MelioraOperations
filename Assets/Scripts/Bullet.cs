using UnityEngine;

public class Bullet : MonoBehaviour
{
    public StatusEffectData statusEffect;
    public float damage;
    public float lifeTime = 1f;
    public float speed = 20f;
    float timer;
    Rigidbody2D rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    virtual public void SetDamage(float dmg)
    {
        damage = dmg;
    }

    virtual public void SetDamage(float dmg, StatusEffectData effect)
    {
        damage = dmg;
        statusEffect = effect;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = lifeTime;
        rb.AddForce(transform.up * speed, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Hit: " + collision.collider.name + " with damage: " + damage);
        if (statusEffect != null)
        {
            Debug.Log("Applying effect: " + statusEffect.nama + " to " + collision.collider.name);
        }
        Destroy(gameObject);
    }
}
