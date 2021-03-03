using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 40f;
    [SerializeField] float jumpSpeed = 7f;
    [SerializeField] AudioClip stepSound;
    Rigidbody2D rb;
    Animator animator;
    AudioSource audioSource;
    CircleCollider2D myFeetCollider;

    bool jump = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        myFeetCollider = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        Movement();
        Jump();
        FlipSprite();
    }
    

    public void StepSound()
    {
    audioSource.PlayOneShot(stepSound);
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(horizontalInput * movementSpeed, rb.velocity.y);
        rb.velocity = playerVelocity;
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));
    }
    private void Jump()
    {
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Foreground"))) { return; }
        if (Input.GetButtonDown("Jump"))
            {
            animator.SetTrigger("Jump");
            Vector2 jumpVelocity = new Vector2(0f, jumpSpeed);
            rb.velocity += jumpVelocity;
            }
    }
    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
        }
        }
}

  

