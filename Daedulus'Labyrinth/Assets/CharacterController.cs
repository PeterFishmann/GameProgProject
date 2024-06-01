using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour
{
    public float moveSpeed = 5f; // speed of movement
    public int health; // player health
    public Slider healthSlider; // reference to the health slider
    public bool isPaused;
    Animator animator;
    int hits;
    public float mouseSensitivity = 450f;
    private float rotationY = 0f;
    public float turnSpeed = 100f;

    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation; // Freeze rotation
        health = 100; // initialize health
        UpdateHealthUI(); // update the health UI
        animator = GetComponent<Animator>();
        hits = 0;
    }

    void Update()
    {
        Vector3 movement = Vector3.zero;
        float turn = 0f;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            movement += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            movement += Vector3.back;
            RotateBackwards();
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
            rb.isKinematic = true; // Make player kinematic to avoid physics interactions
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Door"))
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.isKinematic = false; // Re-enable physics interactions
        }
    }

    void RotateBackwards()
    {
        transform.Rotate(0, 180f, 0);
    }

    void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f; // Pause the game
    }

    void UpdateHealthUI()
    {
        // Update the health slider value
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
