using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class PlayerController : MonoBehaviour, IEffectable
{
    PlayerInputController pic;
    PlayerStat ps;
    StatusEffectData effect;
    float defDistanceRay = 50f;
    float shootTimer = 0f;
    float rotationAngle;
    float lasDir = 1;
    bool canJump = true;
    bool isJump = false;
    bool isReloading = false;
    float currEffectTime = 0f;
    float nextTickTime = 0f;
    float jumpTime = 0.5f;
    float jumpCD = 0.5f;
    float smoothTime = 0.1f;
    float invicibilityDuration = 2f;
    float invicibilityTimer = 0f;
    public float jumpSmoothTime = 0.1f;
    public bool isShooting = false;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] GameObject ship;
    [SerializeField] Transform muzzle;

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        pic = GetComponent<PlayerInputController>();
        ps = GetComponent<PlayerStat>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        invicibilityCountdown();
        Shoot();
    }

    void FixedUpdate()
    {
        HandleEffect();
        Move();
        Aim();
    }

    void Move()
    {
        if (effect != null)
        {
            if (effect.immovable)
            {
                return;
            }
        }
        if (pic.dir.x != 0)
        {
            lasDir = pic.dir.x * -1;
        }
        float currMoveSpeed = pic.dir.x * ps.GetMoveSpeed() * -1;
        rotationAngle -= currMoveSpeed;
        if (effect != null)
        {
            if (effect.movePenalty > 1)
            {
                rotationAngle -= currMoveSpeed / effect.movePenalty;
            }
        }
        Quaternion targetRotation = Quaternion.Euler(0, 0, rotationAngle);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothTime);
    }

    void Aim()
    {
        Vector3 dir = new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y, 0) - Camera.main.WorldToScreenPoint(ship.transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        ship.transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
    }

    public void Shoot()
    {
        if (shootTimer > 0)
        {
            shootTimer -= Time.deltaTime;
        }
        if (isShooting && ps.currentAmunition > 0)
        {
            if (ps.GetWeapon() == PlayerStat.WeaponType.Laser)
            {
                ShootLaser();
            }
            else
            {
                DestroyRay();
                if (shootTimer <= 0)
                {
                    InstatiatingBullets();
                    ps.UpdateAmunition(-1);
                    shootTimer = ps.GetCooldown();
                }
            }
        }
        else
        {
            if (ps.GetWeapon() == PlayerStat.WeaponType.Laser)
            {
                DestroyRay();
            }
        }
        if (ps.currentAmunition <= 0 && !isReloading)
        {
            StartCoroutine(ReloadCoroutine());
        }
    }

    void InstatiatingBullets()
    {
        if (ps.GetWeapon() == PlayerStat.WeaponType.Laser)
        {
            Debug.LogWarning("Laser weapon should not instantiate bullets.");
            return;
        }
        if (ps.GetWeapon() == PlayerStat.WeaponType.RocketLauncher)
        {
            float bulletPerShot = ps.weaponLevel;
            float spreadAngle = 10f;
            float startAngle = -spreadAngle * (bulletPerShot - 1) / 2;
            for (int i = 0; i < bulletPerShot; i++)
            {
                float angle = startAngle + i * spreadAngle;
                Quaternion bulletRotation = ship.transform.rotation * Quaternion.Euler(0, 0, angle);
                GameObject bullInit = Instantiate(ps.GetBulletPrefab(), muzzle.position, bulletRotation);
                bullInit.GetComponent<Bullet>().SetDamage(ps.GetAttackDamage());
            }
        }
        else
        {
            StartCoroutine(ShootBurstCoroutine(ps.weaponLevel, 0.1f));
        }
    }

    IEnumerator ShootBurstCoroutine(int burstCount, float delayBetweenShots)
    {
        for (int i = 0; i < burstCount; i++)
        {
            GameObject bullInit = Instantiate(ps.GetBulletPrefab(), muzzle.position, ship.transform.rotation);
            bullInit.GetComponent<Bullet>().SetDamage(ps.GetAttackDamage());
            yield return new WaitForSeconds(delayBetweenShots);
        }
    }

    void ShootLaser()
    {
        if (shootTimer <= 0)
        {
            shootTimer = ps.GetCooldown();
            ps.UpdateAmunition(-1);
        }
        if (shootTimer > 0)
        {
            shootTimer -= Time.deltaTime;
        }
        if (Physics2D.Raycast(muzzle.position, ship.transform.up, defDistanceRay))
        {
            RaycastHit2D hit = Physics2D.Raycast(muzzle.position, ship.transform.up, defDistanceRay);
            lineRenderer.SetPosition(0, muzzle.position);
            lineRenderer.SetPosition(1, hit.point);
            if (shootTimer > 0 && hit.collider != null)
            {
                if (hit.collider.GameObject().layer == LayerMask.NameToLayer("Default"))
                {
                    Debug.Log("Hit: " + hit.collider.name + " with damage: " + ps.GetAttackDamage());
                    if (ps.fireEffect != null && ps.BurnLevel > 0)
                    {
                        hit.collider.GameObject().GetComponent<IEffectable>()?.ApplyEffect(ps.fireEffect);
                    }
                    if (ps.freezeEffect != null && ps.FreezeLevel > 0)
                    {
                        hit.collider.GameObject().GetComponent<IEffectable>()?.ApplyEffect(ps.freezeEffect);
                    }
                }
            }
        }
        else
        {
            lineRenderer.SetPosition(0, muzzle.position);
            lineRenderer.SetPosition(1, muzzle.position + ship.transform.up * defDistanceRay);
        }
    }

    void DestroyRay()
    {
        lineRenderer.SetPosition(0, muzzle.position);
        lineRenderer.SetPosition(1, muzzle.position);
    }

    IEnumerator ReloadCoroutine()
    {
        isReloading = true;
        yield return new WaitForSeconds(ps.GetReloadTime());
        ps.currentAmunition = ps.GetMagazine();
        isReloading = false;
    }

    public void Jump()
    {
        if (effect != null)
        {
            if (effect.immovable)
            {
                return;
            }
        }
        if (canJump && !isJump)
        {
            StartCoroutine(JumpCoroutine());
        }
    }

    IEnumerator JumpCoroutine()
    {
        canJump = false;
        isJump = true;
        rotationAngle -= lasDir * ps.GetJumpForce();
        Quaternion targetRotation = Quaternion.Euler(0, 0, rotationAngle);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, jumpSmoothTime);
        yield return new WaitForSeconds(jumpTime);
        isJump = false;
        yield return new WaitForSeconds(jumpCD);
        canJump = true;
    }

    public void TakeDamage(int damage)
    {
        if (damage > 0 && invicibilityTimer <= 0)
        {
            if (effect != null)
            {
                if (effect.damageMultiplier > 0)
                {
                    damage = (int)(damage * effect.damageMultiplier);
                }
            }
            ps.UpdateHealth(-damage);
            invicibilityTimer = invicibilityDuration;
        }
    }

    void invicibilityCountdown()
    {
        if (invicibilityTimer > 0)
        {
            invicibilityTimer -= Time.deltaTime;
        }
    }

    public void ApplyEffect(StatusEffectData effect)
    {
        RemoveEffect();
        this.effect = effect;
    }

    public void RemoveEffect()
    {
        effect = null;
        currEffectTime = 0f;
        nextTickTime = 0f;
    }

    public void HandleEffect()
    {
        if (effect != null)
        {
            currEffectTime += Time.deltaTime;
            if (currEffectTime >= effect.lifetime) RemoveEffect();
            if (effect != null)
            {
                if (effect.damage != 0 && currEffectTime > nextTickTime)
                {
                    nextTickTime += effect.tickSpeed;
                    TakeDamage((int)effect.damage);
                }
            }
        }
    }

    public GameObject GetShip()
    {
        return ship;
    }

    public void TestApplyEffect()
    {
        // ApplyEffect(testEffect);
    }
}
