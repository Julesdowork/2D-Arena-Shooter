using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] float fireRate = 0;
    [SerializeField] float damage = 10f;
    [SerializeField] float range = 100f;
    [SerializeField] LayerMask hitLayer;
    [SerializeField] Transform firePoint;
    [SerializeField] Transform bulletTrailPf;
    [SerializeField] Transform muzzleFlashPf;
    [SerializeField] Transform hitEffectPf;

    float timeToFire = 0;

    private void Awake()
    {
        if (firePoint == null)
            Debug.LogError("There's no attached Fire Point on this weapon.");
    }

    // Update is called once per frame
    void Update()
    {
        if (fireRate == 0)      // Semi-automatic
        {
            if (Input.GetButtonDown(TagManager.FIRE1))
                Shoot();
        }
        else
        {
            if (Input.GetButton(TagManager.FIRE1) && Time.time > timeToFire)
            {
                timeToFire = Time.time + 1f / fireRate;
                Shoot();
            }
        }
    }

    void Shoot()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = mousePos - new Vector2(firePoint.position.x, firePoint.position.y);
        dir.Normalize();
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, dir, range, hitLayer);
        Vector3 hitPos;
        Vector3 hitNormal;
        // Debug.DrawLine(firePoint.position, dir * range, Color.cyan);
        // SpawnBulletTrail();
        MuzzleFlash();
        if (hit.collider != null)
        {
            // Debug.DrawLine(firePoint.position, hit.point, Color.red);
            hitPos = hit.point;
            hitNormal = hit.normal;
            SpawnHitEffect(hitPos, hitNormal);

            Enemy enemy = hit.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
        else
        {
            hitPos = dir * range;
            hitNormal = new Vector3(9999, 9999, 9999);
            SpawnHitEffect(hitPos, hitNormal);
        }
    }

    void SpawnBulletTrail()
    {
        Instantiate(bulletTrailPf, firePoint.position, firePoint.rotation);
    }

    void MuzzleFlash()
    {
        Transform muzzleFlash = Instantiate(muzzleFlashPf, firePoint.position,
            firePoint.rotation, firePoint);
        float size = Random.Range(0.3f, 0.45f);
        muzzleFlash.localScale = new Vector3(size, size, size);
        Destroy(muzzleFlash.gameObject, 0.02f);
    }

    void SpawnHitEffect(Vector3 hitPos, Vector3 hitNormal)
    {
        Transform bulletTrail = Instantiate(bulletTrailPf, firePoint.position, Quaternion.identity);
        LineRenderer lr = bulletTrail.GetComponent<LineRenderer>();

        lr.SetPosition(0, firePoint.position);
        lr.SetPosition(1, hitPos);

        if (hitNormal != new Vector3(9999, 9999, 9999))
        {
            Transform hitEffect = Instantiate(hitEffectPf, hitPos,
                Quaternion.FromToRotation(Vector3.right, hitNormal));
        }

        SimpleCamera.instance.ShakeCamera(3f, 0.1f);
    }
}
