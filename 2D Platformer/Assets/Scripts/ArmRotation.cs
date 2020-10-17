using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmRotation : MonoBehaviour
{
    PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();
        float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }
}
