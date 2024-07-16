using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    float moveSpeed = 15f;
    private Rigidbody rb => GetComponent<Rigidbody>();

    public void SetupBullet(float _speed)
    {
        moveSpeed = _speed;
        if (rb != null)
        {
            rb.velocity = transform.forward * moveSpeed;
        }
        Destroy(gameObject, 7f);
    }
}
