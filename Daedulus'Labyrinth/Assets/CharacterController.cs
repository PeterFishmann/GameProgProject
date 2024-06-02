
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
        pauseMenu.SetActive(false);

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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(true);
        }
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
        if (other.gameObject.CompareTag("Enemy"))
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

    public void TryIncreaseSpeed(PauseMenu pauseMenu, float speedIncrease, GameObject powerObject)
    {
        if (pauseMenu.currentNumber >= 2)
        {
            IncreaseSpeed(speedIncrease);
            pauseMenu.currentNumber -= 2;  // Subtract 2 from currentNumber
            pauseMenu.UpdateNumberText();  // Update the displayed number
            Destroy(powerObject);
        }
        else
        {
            Debug.Log("Power cannot be used. The number is less than 2.");
        }
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

    public void TryHeal(PauseMenu pauseMenu, int healAmount, GameObject powerObject)
    {
        if (pauseMenu.currentNumber >= 1 && health < 100)
        {
            int remainingHeal = 100 - health;
            int actualHeal = Mathf.Min(healAmount, remainingHeal);
            Heal(actualHeal);
            pauseMenu.currentNumber--;  // Subtract 1 from currentNumber
            pauseMenu.UpdateNumberText();  // Update the displayed number
            Destroy(powerObject);
        }
        else
        {
            Debug.Log("Healing power cannot be used. The number is less than 1 or player's health is full.");
        }
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
