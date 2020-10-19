using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveUI : MonoBehaviour
{
    [SerializeField] WaveSpawner waveSpawner;
    [SerializeField] TextMeshProUGUI waveCountdownText;
    [SerializeField] TextMeshProUGUI waveNumberText;

    Animator waveAnimator;
    WaveSpawner.SpawnState prevState;

    private void Awake()
    {
        waveAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (waveSpawner.State)
        {
            case WaveSpawner.SpawnState.Counting:
                UpdateCountingUI();
                break;
            case WaveSpawner.SpawnState.Spawning:
                UpdateSpawningUI();
                break;
        }

        prevState = waveSpawner.State;
    }

    void UpdateCountingUI()
    {
        if (prevState != WaveSpawner.SpawnState.Counting)
        {
            waveAnimator.SetBool(TagManager.WAVE_INCOMING, false);
            waveAnimator.SetBool(TagManager.WAVE_COUNTDOWN, true);
        }
        waveCountdownText.text = ((int)waveSpawner.WaveCountdown).ToString();
    }

    void UpdateSpawningUI()
    {
        if (prevState != WaveSpawner.SpawnState.Spawning)
        {
            waveAnimator.SetBool(TagManager.WAVE_COUNTDOWN, false);
            waveAnimator.SetBool(TagManager.WAVE_INCOMING, true);
            waveNumberText.text = waveSpawner.NextWave.ToString();
        }
    }
}
