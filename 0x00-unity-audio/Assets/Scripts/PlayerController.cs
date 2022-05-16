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
    public LayerMask Grass;
    public LayerMask Rock;
    bool isGrounded;
    bool onGrass;
    bool onRock;
    public float jumpHeight = 3f;
    public Animator animator;
    private bool gettingUp = false;
    private float groundedYPos;
    public float fallThreshold = 0.1f;
    public AudioSource grassFootsteps;
    public AudioSource rockFootsteps;
    public AudioSource rockImpact;
    public AudioSource grassImpact;

    private void Start() {
        groundedYPos = transform.position.y;
    }
    // Update is called once per frame
    void Update()
    {
        // creates a sphere at selected position with a radius of ground distance and
        // checks if it collides with an object on the "Grounded" Layer
        onGrass = Physics.CheckSphere(groundCheck.position, groundDistance, Grass);
        onRock = Physics.CheckSphere(groundCheck.position, groundDistance, Rock);
        isGrounded = onGrass || onRock;
        ApplyPhysics();
        Movement();
        playerAudio();
    }
    void ApplyPhysics(){
        // check if the falling flat animation or the getting up animation are playing
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Falling Flat Impact") || animator.GetCurrentAnimatorStateInfo(0).IsName("Getting Up"))
            gettingUp = true;
        else
            gettingUp = false;
        // fell off map
        if (transform.position.y < -20f){
            transform.position = respawn;
            return;
        }
        if(isGrounded)
        {
            if (animator.GetBool("isFalling")){
                animator.SetBool("isFalling", false);
                if(onRock)
                    rockImpact.Play();
                if(onGrass)
                    grassImpact.Play();
            }
            animator.SetBool("isJumping", false);
            groundedYPos = transform.position.y;
        }
        else{
            // checks if the y position is lower than a threshold of the last grounded position
            if(transform.position.y < groundedYPos - fallThreshold){
                Debug.Log("Falling");
                animator.SetBool("isFalling", true);
            }
        }
        // reset y velocity when not falling
        if (isGrounded && velocity.y < 0){
            velocity.y = -2f;
        }
        // apply gravity to the player
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    void Movement(){
        // get inputs using Unity's built in controls
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        // get movement direction from input
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        if (direction.magnitude >= 0.1f && !gettingUp) {
            animator.SetBool("isRunning", true);
            // calculate the angle the player should face
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            // make rotation smooth
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity,turnSmoothTime);
            // changes the player rotation
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            // calculate movement
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            // move the player
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
        else {
            animator.SetBool("isRunning", false);
        }
        if(Input.GetButtonDown("Jump"))
            Debug.Log($"Grounded: {isGrounded}, Getting Up: {gettingUp}");
        // jump using physics
        if (Input.GetButtonDown("Jump") && isGrounded && !gettingUp){
            Debug.Log("Jumping");
            animator.SetBool("isJumping", true);
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
    }
    void playerAudio(){
        if(animator.GetBool("isRunning")){
            if (onGrass){
                if(rockFootsteps.isPlaying)
                    rockFootsteps.Stop();
                if (!grassFootsteps.isPlaying)
                    grassFootsteps.Play();
            }
            else if(onRock){
                if(grassFootsteps.isPlaying)
                    grassFootsteps.Stop();
                if (!rockFootsteps.isPlaying)
                    rockFootsteps.Play();
            }
            else {
                if(grassFootsteps.isPlaying)
                    grassFootsteps.Stop();
                if(rockFootsteps.isPlaying)
                    rockFootsteps.Stop();
            }
        }
        else{
            if(grassFootsteps.isPlaying)
                grassFootsteps.Stop();
            if(rockFootsteps.isPlaying)
                rockFootsteps.Stop();
        }
    }
}
