using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

// I may not need this class anymore...we'll see
public class SimpleCamera : MonoBehaviour
{
    public static SimpleCamera instance;

    CinemachineVirtualCamera virtualCamera;

    float shakeTimer;
    float shakeTimerTotal;
    float startingIntensity;
    
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0)
            {
                // Timer over!
                CinemachineBasicMultiChannelPerlin basicMultiChannelPerlin =
                    virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                basicMultiChannelPerlin.m_AmplitudeGain = 
                    Mathf.Lerp(startingIntensity, 0, 1 - (shakeTimer / shakeTimerTotal));
            }
        }
    }

    public void FindPlayer()
    {
        virtualCamera.m_Follow = GameObject.FindGameObjectWithTag(TagManager.PLAYER).transform;
    }

    public void ShakeCamera(float intensity, float timer)
    {
        CinemachineBasicMultiChannelPerlin basicMultiChannelPerlin =
            virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        basicMultiChannelPerlin.m_AmplitudeGain = intensity;
        startingIntensity = intensity;
        shakeTimerTotal = timer;
        shakeTimer = timer;
    }
}
