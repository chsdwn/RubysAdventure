using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    public GameObject projectilePrefab;
    public AudioClip throwCogClip, playerHitClip;
    public float speed = 3f;
    public int maxHealth = 5;
    public int health { get { return currentHealth; } }
    public float timeInvincible = 2f;

    Rigidbody2D rigidbody2d;
    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);
    AudioSource audioSource;
    float horizontal;
    float vertical;
    int currentHealth;
    float invincibleTimer;
    bool isInvincible;

    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        currentHealth = maxHealth;
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0f) || !Mathf.Approximately(move.y, 0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Launch();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit2D hit = Physics2D.Raycast(
                rigidbody2d.position + Vector2.up * .2f,
                lookDirection,
                1f,
                LayerMask.GetMask("NPC"));

            if (hit.collider != null)
            {
                NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();

                if (character != null)
                    character.DisplayDialog();
            }

        }
    }

    private void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = timeInvincible;

            PlaySound(playerHitClip);
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHealthBar.instance.SetValue((float)currentHealth / maxHealth);


        animator.SetTrigger("Hit");
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * .5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        PlaySound(throwCogClip);
        animator.SetTrigger("Launch");
    }
}
