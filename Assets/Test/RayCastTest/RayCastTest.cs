using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastTest : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float toTarget;

    void Update()
    {
        float dx = Input.GetAxis("Horizontal");
        float dy = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(dx, 0f, dy);
        transform.Translate(dir.normalized * moveSpeed * Time.deltaTime);

        //RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out var hit))
        {
            toTarget = hit.distance;
        }
    }
    void OnDrawGizmosSelected()
    {
        float maxDistance = 100f;

        Gizmos.color = Color.red;
        bool isHit = Physics.Raycast(transform.position, transform.forward, out var hit, maxDistance);
        if (isHit)
        {
            Gizmos.DrawRay(transform.position, transform.forward * hit.distance);
        }
        else
        {
            Gizmos.DrawRay(transform.position, transform.forward * maxDistance);

        }
    }
}
