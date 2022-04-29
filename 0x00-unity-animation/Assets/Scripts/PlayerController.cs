using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public Vector3 respawn = new Vector3(0f, 20f, 0f);
    public CharacterController controller;
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public Transform cam;
    public float gravity = -9.81f;
    Vector3 velocity;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask Platforms;
    bool isGrounded;
    public float jumpHeight = 3f;
    public Animator animator;
    private bool gettingUp = false;


    // Update is called once per frame
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Falling Flat Impact") || animator.GetCurrentAnimatorStateInfo(0).IsName("Getting Up"))
            gettingUp = true;
        else
            gettingUp = false;
        if (transform.position.y < -20f){
            transform.position = respawn;
            return;
        }
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, Platforms);
        if(isGrounded)
        {
            animator.SetBool("isFalling", false);
            animator.SetBool("isJumping", false);
        }
        else{
            animator.SetBool("isFalling", true);
        }
        if (isGrounded && velocity.y < 0){
            velocity.y = -2f;
        }
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        if (direction.magnitude >= 0.1f && !gettingUp) {
            animator.SetBool("isRunning", true);
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity,turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
        else {
            animator.SetBool("isRunning", false);
        }
        if (Input.GetButtonDown("Jump") && isGrounded && !gettingUp){
            animator.SetBool("isJumping", true);
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
