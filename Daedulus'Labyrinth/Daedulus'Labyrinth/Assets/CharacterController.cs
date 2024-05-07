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
    public GameObject Enemy;


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

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            movement += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            movement += Vector3.back;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            movement += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            movement += Vector3.right;
        }
        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.Space))
        {
            animator.Play("Attack02_SwordAndShiled");
        }

        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        //if()
        Debug.Log("Vlad");
        hits++;
        Debug.Log(hits);
        if(hits == 4)
        {
            Destroy(Enemy);  
        }
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
