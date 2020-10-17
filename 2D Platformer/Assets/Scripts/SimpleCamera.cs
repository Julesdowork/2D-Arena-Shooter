using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

// I may not need this class anymore...we'll see
public class SimpleCamera : MonoBehaviour
{
    CinemachineVirtualCamera virtualCamera;
    
    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void FindPlayer()
    {
        virtualCamera.m_Follow = GameObject.FindGameObjectWithTag(TagManager.PLAYER).transform;
    }
}
