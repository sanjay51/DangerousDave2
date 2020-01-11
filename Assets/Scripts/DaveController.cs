using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaveController : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    Animator animator;
    AudioSource audioSource;

    public float speed = 3.3f;
    Vector2 lookDirection = new Vector2(1, 0);
    private bool isJumping = false;
    private bool isLookingRight = true;
    private bool isDead = false;
    private float deathTimer = 1.0f;

    public static GameState gameState = new GameState();
    public GameObject bulletPrefab;
    public AudioClip bulletClip;
    public AudioClip jetpackClip;

    private static int livesLeft = 3;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        animator.SetFloat("Regular", 3.0f);
        animator.SetFloat("Special", 0.0f);

        HealthController.instance.setLives(livesLeft);

        HealthController.instance.setJetpackFuel(0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }

        RefreshVariables();
        HandleBullet();
        HandleJetpack();
        Move();
        HandleAnimation();
        HandleDeath();
        HandleExit();
    }

    void RefreshVariables()
    {
        RaycastHit2D hit = Physics2D.Raycast(rigidbody2D.position, Vector2.down, 0.9f, LayerMask.GetMask("Tiles"));
        if (hit.collider == null) { isJumping = true; } else { isJumping = false; }

        float horizontal = Input.GetAxis("Horizontal");
        if (horizontal > 0.0f) isLookingRight = true;
        else if (horizontal < 0.0f) isLookingRight = false;
    }

    void HandleAnimation()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // By default, looking right;
        float regular = 3.0f;
        float special = 0.0f;

        if (isDead)
        {
            // Dead
            regular = 3;
            special = 1;

        } else
        if (gameState.isJetpackRunning() && horizontal >= 0.0)
        {
            // Jetpack right
            regular = 1;
            special = 1;

        } else if (gameState.isJetpackRunning() && horizontal < 0.0)
        {
            // Jetpack left
            regular = -1;
            special = 1;
        } else if (gameState.isClimbing())
        {
            // Climbing
            regular = 2;
            special = 1;

        }
        else if (isJumping && horizontal < -0.1)
        {
            // Jumping Left
            regular = 5;
            special = 0;
        }
        else if (isJumping && horizontal < 0.0)
        {
            // Jumping Left
            regular = 2;
            special = 0;
        }
        else if (isJumping && horizontal > 0.1)
        {
            // Jumping Right
            regular = 6;
            special = 0;
        }
        else if (isJumping && horizontal > 0.0)
        {
            // Jumping Right
            regular = 3;
            special = 0;
        }
        else if (horizontal <= -0.1)
        {
            // Walking left
            regular = 1;
            special = 0;
        }
        else if (horizontal >= 0.1)
        {
            // Walking right
            regular = 4;
            special = 0;
        }
        else if (isLookingRight)
        {
            // Looking right
            regular = 3;
            special = 0;
        }
        else if (!isLookingRight)
        {
            // Looking right
            regular = 2;
            special = 0;
        }

        animator.SetFloat("Regular", regular);
        animator.SetFloat("Special", special);
    }

    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (!Mathf.Approximately(horizontal, 0.0f))
        {
            lookDirection.Set(horizontal, 0);
            lookDirection.Normalize();
        }

        Vector2 position = transform.position;
        position.x += speed * horizontal * Time.deltaTime;

        if (vertical != 0.0f && gameState.isJetpackRunning())
        {
            position.y += speed * vertical * Time.deltaTime;
        }

        transform.position = position;
    }

    void Jump()
    {
        if (gameState.isJetpackRunning()) return;
        if (isJumping) return;

        rigidbody2D.AddForce(transform.up * 325);
        
    }

    void HandleJetpack()
    {
        if (Input.GetKeyDown(KeyCode.LeftCommand))
        {
            gameState.ToggleJetpack();
        }

        if (gameState.isJetpackRunning())
        {
            gameState.levelState.jetpackPower -= 8.0f * Time.deltaTime;

            HealthController.instance.setJetpackFuel(gameState.levelState.jetpackPower / 100.0f);
            rigidbody2D.gravityScale = 0;
        } else rigidbody2D.gravityScale = 1;
    }

    void HandleBullet()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            if (gameState.levelState.hasGun)
            {
                GameObject bullet = Instantiate(bulletPrefab, rigidbody2D.position, Quaternion.identity);
                BulletController bulletController = bullet.GetComponent<BulletController>();
                bulletController.Launch(lookDirection, 500);
                this.PlaySound(this.bulletClip);
            }
        }
    }

    void HandleExit()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            Application.Quit();
    }

    public void Die()
    {
        isDead = true;
    }

    private void HandleDeath()
    {
        if (isDead && deathTimer <= 0)
        {
            HealthController.instance.setLives(--livesLeft);
            deathTimer = 1.0f;
            isDead = false;
            gameState.ResetLevel();
        } else if (isDead)
        {
            rigidbody2D.simulated = false;
            deathTimer -= Time.deltaTime;
            Debug.Log(deathTimer);
        }
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
