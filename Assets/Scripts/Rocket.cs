using UnityEngine;

public class Rocket : Bullet
{
    [SerializeField] GameObject explosionEffectPrefab;
    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject initExplode = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        initExplode.GetComponent<Explode>().SetDamage(damage);
        Destroy(gameObject);
    }
}
