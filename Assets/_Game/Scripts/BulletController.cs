using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    float moveSpeed = 2000f;
    private Rigidbody rb => GetComponent<Rigidbody>();

    public void SetupBullet(float _speed)
    {
        moveSpeed = _speed;
        if (rb != null)
        {
            rb.velocity = transform.forward * moveSpeed * Time.deltaTime;
        }
        Destroy(gameObject, 7f);
    }
}
