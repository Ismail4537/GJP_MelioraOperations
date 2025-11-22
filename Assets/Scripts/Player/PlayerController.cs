using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    PlayerInputController pic;
    PlayerStat ps;
    float defDistanceRay = 50f;
    float jumpForce = 50f;
    float shootTimer = 0f;
    float rotationAngle;
    float lasDir = 1;
    bool canJump = true;
    bool isJump = false;
    float jumpTime = 0.5f;
    float jumpCD = 0.5f;
    float smoothTime = 0.1f;
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

    }

    void FixedUpdate()
    {
        Move();
        Aim();
        Shoot();
    }

    void Move()
    {
        if (pic.dir.x != 0)
        {
            lasDir = pic.dir.x * -1;
        }
        rotationAngle -= pic.dir.x * ps.GetMoveSpeed() * -1;
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
        if (isShooting)
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
                    GameObject bullInit = Instantiate(ps.GetBulletPrefab(), muzzle.position, ship.transform.rotation);
                    bullInit.GetComponent<Bullet>().SetDamage(ps.GetAttackDamage());
                    shootTimer = ps.GetCooldown();
                }
                if (shootTimer > 0)
                {
                    shootTimer -= Time.deltaTime;
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
    }

    void ShootLaser()
    {
        if (Physics2D.Raycast(muzzle.position, ship.transform.up, defDistanceRay))
        {
            RaycastHit2D hit = Physics2D.Raycast(muzzle.position, ship.transform.up, defDistanceRay);
            Debug.Log("Hit: " + hit.collider.name);
            lineRenderer.SetPosition(0, muzzle.position);
            lineRenderer.SetPosition(1, hit.point);
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

    public void Jump()
    {
        if (canJump && !isJump)
        {
            StartCoroutine(JumpCoroutine());
        }
    }

    IEnumerator JumpCoroutine()
    {
        Debug.Log("Jump!");
        canJump = false;
        isJump = true;
        rotationAngle -= lasDir * jumpForce;
        Quaternion targetRotation = Quaternion.Euler(0, 0, rotationAngle);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, jumpSmoothTime);
        yield return new WaitForSeconds(jumpTime);
        isJump = false;
        yield return new WaitForSeconds(jumpCD);
        canJump = true;
    }
}
