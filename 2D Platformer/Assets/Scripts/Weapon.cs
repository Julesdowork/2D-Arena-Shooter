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
        // Vector3 mousePosV3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Vector2 mousePos = new Vector2(mousePosV3.x, mousePosV3.y);
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = mousePos - new Vector2(firePoint.position.x, firePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, dir, range, hitLayer);
        // Debug.DrawLine(firePoint.position, dir * range, Color.cyan);
        SpawnBulletTrail();
        MuzzleFlash();
        if (hit.collider != null)
        {
            // Debug.DrawLine(firePoint.position, hit.point, Color.red);
            // Debug.Log("Fired at " + hit.collider.name + " and did " + damage + " damage");
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
}
