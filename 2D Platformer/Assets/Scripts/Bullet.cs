using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] int moveSpeed = 230;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        Destroy(gameObject, 1f);
    }
}
