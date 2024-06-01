using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public int health;
    public Slider healthSlider;

    public bool isPaused;
    Animator animator;
    int hits;

    public float mouseSensitivity = 250f;
    private float rotationY = 0f;
    public float turnSpeed = 100f;

    private bool isSpeedBoosted = false;
    private float speedBoostDuration = 15f;
    private float speedBoostTimer = 0f;

    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        health = 50;
        UpdateHealthUI();
        animator = GetComponent<Animator>();
        hits = 0;
    }

    void Update()
    {
        if (isSpeedBoosted)
        {
            speedBoostTimer -= Time.deltaTime;
            if (speedBoostTimer <= 0)
            {
                isSpeedBoosted = false;
                moveSpeed = 5f;
            }
        }

        Vector3 movement = Vector3.zero;
        float turn = 0f;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            movement += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            movement += Vector3.back;
            //RotateBackwards();
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            movement += Vector3.left;
            turn = -1f;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            movement += Vector3.right;
            turn = 1f;
        }
        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.Space))
        {
            animator.Play("Attack02_SwordAndShiled");
        }

        transform.Translate(movement * moveSpeed * Time.deltaTime);
        transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        rotationY += mouseX;
        transform.rotation = Quaternion.Euler(0, rotationY, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wings"))
        {
            if (!isSpeedBoosted)
            {
                IncreaseSpeed(5f);
                Destroy(other.gameObject);
            }
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            hits++;
            if (hits == 4)
            {
                Destroy(other.gameObject);
                hits = 0;
                SceneManager.LoadScene("MainDungeon");
            }
        }
        else if (other.gameObject.CompareTag("Door"))
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.isKinematic = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Door"))
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.isKinematic = false;
        }
    }

    void RotateBackwards()
    {
        transform.Rotate(0, 180f, 0);
    }

    public void IncreaseSpeed(float amount)
    {
        moveSpeed += amount;
        isSpeedBoosted = true;
        speedBoostTimer = speedBoostDuration;
    }

    public void Heal(int amount)
    {
        health += amount;
        if (health > 100)
        {
            health = 100;
        }
        UpdateHealthUI();
    }

    void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
    }

    void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.value = health;
        }
    }

    void Attack()
    {
        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.Space))
        {
            animator.Play("Attack02_SwordAndShiled");
        }
    }
}
