using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class JumpController : MonoBehaviour
{
    [Header("Jump System")]
    Rigidbody2D dinoRigidbody;

    Animator dinoAnimator;

    [SerializeField] ParticleSystem dustParticles;

    AudioSource dinoAudioSource;
    [SerializeField] AudioClip jumpSfx;

    [SerializeField] int jumpPower;
    [SerializeField] float fallMultiplier;

    DinoScript dino;

    bool isJumping;
    bool isGrounded;

    void Start()
    {
        dino = GetComponent<DinoScript>();
        dinoAnimator = GetComponent<Animator>();
        dinoRigidbody = GetComponent<Rigidbody2D>();
        dinoAudioSource = GetComponent<AudioSource>();

    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0))
        {
            if (isGrounded && dino.CanJump())
            {
                isJumping = true;

                dinoRigidbody.velocity = new Vector2(dinoRigidbody.velocity.x, jumpPower);

                dinoAudioSource.clip = jumpSfx;
                dinoAudioSource.Play();
            }
        }

        if (Input.GetButtonUp("Jump") || Input.GetMouseButtonUp(0))
        {
            isJumping = false;
        }

        if (dinoRigidbody.velocity.y > 0 && !isJumping)
        {
            dinoRigidbody.velocity -= fallMultiplier * Time.deltaTime * Vector2.up;
        }
    }

    /// <summary>
    /// Check if the player collided with Ground
    /// </summary>
    /// <param name="other"></param>
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            dinoAnimator.SetBool("isJump", !isGrounded);
            dustParticles.Play();
        }
    }

    /// <summary>
    /// Check if the player moved away from ground
    /// </summary>
    /// <param name="other"></param>
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            dinoAnimator.SetBool("isJump", !isGrounded);
            dustParticles.Play();
        }
    }



}
