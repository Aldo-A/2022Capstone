using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSMovement : MonoBehaviour
{
    [SerializeField] float speed;
    Rigidbody rb;
    [SerializeField] float sprintMultiplier = 1.5f;
    [SerializeField] float jumpForce;
    static Animator animator;


    void Start() {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update() {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Debug.Log(x+" "+z);
        if (x != 0 || z != 0)
        {
            animator.SetBool("isWalking", true);
            Debug.Log("ENTERED");
            Vector3 moveBy = transform.right * x + transform.forward * z;

            float actualSpeed = speed;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                actualSpeed *= sprintMultiplier;
            }

            rb.MovePosition(transform.position + moveBy.normalized * actualSpeed * Time.deltaTime);

        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetBool("isJumping", true);
        }
        else
        {
            animator.SetBool("isJumping", false);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            animator.SetBool("Defend", true);
        }
        else
        {
            animator.SetBool("Defend", false);
        }

        if (Input.GetMouseButton(0))
        {
            animator.SetBool("Attack", true);
        }
        else
        {
            animator.SetBool("Attack", false);
        }

        if (Input.GetMouseButton(1))
        {
            animator.SetBool("StrongAttack", true);
        }
        else
        {
            animator.SetBool("StrongAttack", false);
        }


    }

}
