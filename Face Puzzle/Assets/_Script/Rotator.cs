using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 500f;
    private bool isDragging;
    private Rigidbody rb;

    public GameObject target;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnMouseDrag()
    {
        isDragging = true;
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            target.transform.Rotate(180, 0, 0, Space.World);
        }
    }

    private void FixedUpdate()
    {
        if (isDragging)
        {
            float x = Input.GetAxis("Mouse X") * rotationSpeed * Time.fixedDeltaTime;
            float y = Input.GetAxis("Mouse Y") * rotationSpeed * Time.fixedDeltaTime;

            rb.AddTorque(Vector3.down * x);
            rb.AddTorque(Vector3.right * y);
            rb.rotation = Quaternion.RotateTowards(transform.rotation, target.transform.rotation, 1 * Time.deltaTime);
        }
    }
}