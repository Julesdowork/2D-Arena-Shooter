using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LivesCounterUI : MonoBehaviour
{
    TextMeshProUGUI livesText;

    private void Awake()
    {
        livesText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateLives()
    {
        livesText.text = GameManager.instance.Lives.ToString();
    }
}
