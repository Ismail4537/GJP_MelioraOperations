using System.Collections;
using UnityEngine;

public class Explode : MonoBehaviour
{
    Animator anim;
    float damage;
    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        StartCoroutine(DestroyWhenAnimationEnds());
    }
    public void SetDamage(float dmg)
    {
        damage = dmg;
    }
    IEnumerator DestroyWhenAnimationEnds()
    {
        AnimatorStateInfo CurrentAnimatoninfo = anim.GetCurrentAnimatorStateInfo(0);
        float clipLength = CurrentAnimatoninfo.length;
        yield return new WaitForSeconds(clipLength);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hit: " + collision.name + " with damage: " + damage);
    }
}