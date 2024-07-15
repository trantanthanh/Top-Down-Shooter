using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement info")]
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float rotationSpeed = 2f;
    float verticalInput;
    float horizontalInput;

    [Space]
    [SerializeField] LayerMask whatIsAimMask;
    public Transform aimTransform;

    #region Components
    Rigidbody rb;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        verticalInput = Input.GetAxis("Vertical");

        if (verticalInput < 0)
        {
            horizontalInput = -Input.GetAxis("Horizontal");
        }
        else
        {
            horizontalInput = Input.GetAxis("Horizontal");
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.forward * moveSpeed * verticalInput;
        transform.Rotate(0, horizontalInput * rotationSpeed, 0);

        UpdateAim();
    }

    private void UpdateAim()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, whatIsAimMask))
        {
            float fixedY = aimTransform.position.y;
            aimTransform.position = new Vector3(hitInfo.point.x, fixedY, hitInfo.point.z);
        }
    }

    void OnDrawGizmos()
    {
        if (Camera.main == null)
            return;

        Vector3 mousePos = Input.mousePosition;

        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        Gizmos.color = Color.red;

        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, whatIsAimMask))
        {
            Gizmos.DrawLine(ray.origin, hitInfo.point);
            Gizmos.DrawSphere(hitInfo.point, 0.1f);
        }
        else
        {
            Gizmos.DrawLine(ray.origin, ray.origin + ray.direction * 1000f);
        }
    }
}
