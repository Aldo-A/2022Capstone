using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCrouch : MonoBehaviour
{
    [SerializeField] float crouchHeight;
    [SerializeField] float lyingHeight;
    [SerializeField] float normalHeight;
    static Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Vector3 newScale = new Vector3(transform.localScale.x, normalHeight, transform.localScale.z);

        if (Input.GetKey(KeyCode.C)) {
            animator.SetBool("isCrouched", true);
            newScale.y = crouchHeight;
        } else if (Input.GetKey(KeyCode.Z)) {
            //  newScale.y = lyingHeight;
        }
        else
        {
            animator.SetBool("isCrouched", false);
        }

        transform.localScale = newScale;
    }
}
