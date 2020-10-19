using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] float currentHealth;
    float maxHealth = 100f;

    [SerializeField] RectTransform healthBarRect;
    [SerializeField] TextMeshProUGUI healthText;

    // Start is called before the first frame update
    void OnEnable()
    {
        currentHealth = maxHealth;
    }

    public void DamagePlayer(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        SetHealthBar();
        
        if (currentHealth <= 0)
            GameManager.instance.KillPlayer();
    }

    void SetHealthBar()
    {
        float healthPct = currentHealth / maxHealth;
        healthBarRect.localScale = new Vector3(healthPct,
            healthBarRect.localScale.y, healthBarRect.localScale.z);

        healthText.text = currentHealth + " / " + maxHealth;
    }
}
