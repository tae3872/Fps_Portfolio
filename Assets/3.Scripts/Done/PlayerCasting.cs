using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCasting : MonoBehaviour
{
    public static float distanceFromTarget;
    public float toTarget;
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out var hit))
        {
            distanceFromTarget = hit.distance;
            toTarget = distanceFromTarget;
        }
    }
    void OnDrawGizmosSelected()
    {
        float maxDistance = 100f;
        Gizmos.color = Color.red;
        bool isHit = Physics.Raycast(transform.position, transform.forward, out var hit);
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
