using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] float maxHealth = 40f;
    float currentHealth;
    [SerializeField] float power = 10f;

    [SerializeField] RectTransform healthBarRect;
    [SerializeField] Transform deathEffectPf;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBarRect.gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag(TagManager.PLAYER))
        {
            PlayerStats player = other.collider.GetComponent<PlayerStats>();
            player.DamagePlayer(power);
            KillEnemy();
        }
    }

    public void TakeDamage(float damage)
    {
        if (!healthBarRect.gameObject.activeInHierarchy)
            healthBarRect.gameObject.SetActive(true);

        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        SetHealthBar();

        if (currentHealth <= 0)
            KillEnemy();
    }

    void SetHealthBar()
    {
        float healthPct = currentHealth / maxHealth;
        healthBarRect.localScale = new Vector3(healthPct,
            healthBarRect.localScale.y, healthBarRect.localScale.z);
    }

    public void KillEnemy()
    {
        Instantiate(deathEffectPf, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
