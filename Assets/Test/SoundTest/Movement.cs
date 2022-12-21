using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 30f;
    Rigidbody rb;
    public float forwardForce = 500f;
    public float sideForce = 400f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        rb.AddForce(0, 0, forwardForce * Time.deltaTime);
        float dx = Input.GetAxis("Horizontal");
        if (dx < 0)
        {
            rb.AddForce(-sideForce * Time.deltaTime, 0, 0);
        }
        if (dx > 0)
        {
            rb.AddForce(sideForce * Time.deltaTime, 0, 0);
        }
    }
}
