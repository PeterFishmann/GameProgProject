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
    int hits;
    public bool isPaused;
    Animator animator;
    AudioSource audio;
    bool isAttacking;
    public float mouseSensitivity = 250f;
    private float rotationY = 0f;
    public float turnSpeed = 100f;
    private float attack03Timer = 0f;
    private bool isSpeedBoosted = false;
    private float speedBoostDuration = 15f;
    private float speedBoostTimer = 0f;
    
    private bool isRoll = false;
    public float dashDistance = 2f;
    public float dashSpeed = 15f; // Time in seconds to complete the dash

    public GameObject sword;

    [SerializeField] GameObject pauseMenu;

    void Start()
    {
        // Initialize character settings
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        health = 50;
        UpdateHealthUI();
        animator = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        isAttacking = false;
        hits = 0;
        pauseMenu.SetActive(false);

        if (sword == null)
        {
            Debug.LogWarning("Sword reference is not assigned in the Inspector!");
        }
        else
        {
            sword.SetActive(false);
        }
    }

    void Update()
    {
        // Handle speed boost timer
        if (isSpeedBoosted)
        {
            speedBoostTimer -= Time.deltaTime;
            if (speedBoostTimer <= 0)
            {
                isSpeedBoosted = false;
                moveSpeed = 5f;
            }
        }

        // Update attack cooldown timer
        if (attack03Timer > 0)
        {
            attack03Timer -= Time.deltaTime;
        }

        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(true); // Open pause menu
        }

        //movement
        Vector3 movement = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.LeftShift) && isRoll == false)
        {
            StartCoroutine(Dash());
        }
        else if(isRoll == false)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                movement += Vector3.forward;
                animator.Play("MoveFWD_Normal_RM_SwordAndShield");
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                movement += Vector3.back;
                animator.Play("MoveBWD_Battle_RM_SwordAndShield");
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                movement += Vector3.left;
                animator.Play("MoveLFT_Battle_RM_SwordAndShield");
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                movement += Vector3.right;
                animator.Play("MoveRGT_Battle_RM_SwordAndShield");
            }
            //normalize the value so that strafing isnt possible
            if (movement.magnitude > 1)
            {
                movement.Normalize();
            }
        }

        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.Space))
        {
            animator.Play("Attack02_SwordAndShiled");
            audio.Play();
            isAttacking = true;
        }

        if (Input.GetKey(KeyCode.E))
        {
            if (attack03Timer <= 0)
            {
                animator.Play("Attack03_SwordAndShiled");
                attack03Timer = 15f;
                Debug.Log("Attack03 executed, cooldown started");
            }
            else
            {
                Debug.Log("Attack03 is on cooldown: " + attack03Timer + " seconds remaining");
            }
        }

        transform.Translate(movement * moveSpeed * Time.deltaTime);

        // Handle mouse rotation
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        rotationY += mouseX;
        transform.rotation = Quaternion.Euler(0, rotationY, 0);
    }

    //roll IEnumerator to calculate distance
    IEnumerator Dash()
    {
        isRoll = true;
        float remainingDistance = dashDistance;
        Vector3 direction = transform.forward;

        while (remainingDistance > 0f)
        {
            float moveDistance = dashSpeed * Time.deltaTime;
            RaycastHit hit;
            animator.Play("Attack04_Start_SwordAndShield");
            if (Physics.Raycast(transform.position, direction, out hit, moveDistance))
            {
                transform.position = hit.point;
                break;
            }
            else
            {
                // Move forward by moveDistance
                transform.position += direction * moveDistance / 2;
                remainingDistance -= moveDistance;
            }

            yield return null;
        }
        isRoll = false;
    }


    void OnTriggerEnter(Collider other)
    {
        // Handle collisions with various objects
        if (isAttacking)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                NpcController enemyController = other.GetComponent<NpcController>();
                if (enemyController != null)
                {
                    enemyController.TakeDamage(25);
                }
            }
            isAttacking = false;
        }
        if (other.gameObject.name == "Grunt")
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
        else if (other.gameObject.CompareTag("SwordActivator"))
        {
            ToggleSword();
            Destroy(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Handle exiting collisions with doors
        if (other.gameObject.CompareTag("Door"))
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.isKinematic = false;
        }
    }

    void RotateBackwards()
    {
        // Rotate the character backwards
        transform.Rotate(0, 180f, 0);
    }

    public void IncreaseSpeed(float amount)
    {
        // Increase character's speed temporarily
        moveSpeed += amount;
        isSpeedBoosted = true;
        speedBoostTimer = speedBoostDuration;
    }

    public void TryIncreaseSpeed(PauseMenu pauseMenu, float speedIncrease, GameObject powerObject)
    {
        // Attempt to increase speed if conditions are met
        if (pauseMenu.currentNumber >= 2)
        {
            IncreaseSpeed(speedIncrease);
            pauseMenu.currentNumber -= 2;
            pauseMenu.UpdateNumberText();
            Destroy(powerObject);
        }
        else
        {
            Debug.Log("Power cannot be used. The number is less than 2.");
        }
    }

    public void Heal(int amount)
    {
        // Heal the character
        health += amount;
        if (health > 100)
        {
            health = 100;
        }
        UpdateHealthUI();
    }

    public void TryHeal(PauseMenu pauseMenu, int healAmount, GameObject powerObject)
    {
        // Attempt to heal if conditions are met
        if (pauseMenu.currentNumber >= 1 && health < 100)
        {
            int remainingHeal = 100 - health;
            int actualHeal = Mathf.Min(healAmount, remainingHeal);
            Heal(actualHeal);
            pauseMenu.currentNumber--;
            pauseMenu.UpdateNumberText();
            Destroy(powerObject);
        }
        else
        {
            Debug.Log("Healing power cannot be used. The number is less than 1 or player's health is full.");
        }
    }

    void PauseGame()
    {
        // Pause the game
        isPaused = true;
        Time.timeScale = 0f;
    }

    void UpdateHealthUI()
    {
        // Update health UI
        if (healthSlider != null)
        {
            healthSlider.value = health;
        }
    }

    void ToggleSword()
    {
        // Toggle sword visibility
        sword.SetActive(!sword.activeSelf);
    }

    public void ActivateSword()
    {
        // Activate sword
        if (sword != null)
        {
            sword.SetActive(true);
        }
    }
}
