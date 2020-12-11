using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class NewMovement : MonoBehaviour
{
    // ========= MOVEMENT =================
    public float speed = 4;
    public Text countText;
    public Text winText;
    public Joystick joystick;
    private Rigidbody2D rb;
    public static NewMovement instance;

    private void Awake()
    {
        instance = this;
    }

    // ======== HEALTH ==========
    public int maxHealth = 4;
    public int currentHealth;
    public HealthBar healthBar;

    public Transform respawnPosition;

    private int count;



    // ======== HEALTH ==========
    public int health
    {
        get { return currentHealth; }
    }

    // =========== MOVEMENT ==============
    Rigidbody2D rigidbody2d;
    Vector2 currentInput;

    // ======== HEALTH ==========

    float invincibleTimer;
    bool isInvincible;

    // ==== ANIMATION =====
    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);

    // ================= SOUNDS =======================
    AudioSource audioSource;

    void Start()
    {
        // =========== MOVEMENT ==============
        rigidbody2d = GetComponent<Rigidbody2D>();

        // ======== HEALTH ==========
        invincibleTimer = -1.0f;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        // ==== ANIMATION =====
        animator = GetComponent<Animator>();

        count = 0;
        winText.text = "";
        SetCountText();

    }

    void Update()
    {
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    
       

        // ============== MOVEMENT ======================
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;

        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        currentInput = move;


        // ============== ANIMATION =======================

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);


    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;

        position = position + currentInput * speed * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }




    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
    }

    void SetCountText()
    {
        countText.text = "Coins: " + count.ToString();
        if (count >= 4)
        {
            winText.text = "You have opened the door!";
        }
    }

    public void Damage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }

    void Die()
    {
        Application.LoadLevel(Application.loadedLevel);

    }

    public IEnumerator Knockback(float knockbackDuration, float knockbackPower, Transform obj)
    {

        float timer = 0;

        while (knockbackDuration > timer)
        {

            timer += Time.deltaTime;
            Vector2 direction = (obj.transform.position - this.transform.position).normalized;
            rb.AddForce(-direction * knockbackPower);
            

        }

        yield return 0;

    }



}




