using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTest : MonoBehaviour
{
    Animator animator;
    public float moveSpeed = 5f;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        float dx = Input.GetAxis("Horizontal");
        float dy = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(dx, 0, dy);
        if (dir != Vector3.zero)
        {
            transform.Translate(dir.normalized * moveSpeed * Time.deltaTime, Space.World);
            if (dx > 0)
            {
                animator.SetInteger("MoveMode", 4);
            }
            if (dx < 0)
            {
                animator.SetInteger("MoveMode", 3);
            }
            if (dy > 0)
            {
                animator.SetInteger("MoveMode", 1);
            }
            if (dy < 0)
            {
                animator.SetInteger("MoveMode", 2);
            }
        }
        else
        {
            animator.SetInteger("MoveMode", 0);
        }
    }
}
