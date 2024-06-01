using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeraController : MonoBehaviour
{
    public int healAmount = 10;
    public float rotationSpeed = 25f;
    public float bobHeight = 0.25f;
    public float bobSpeed = 1f;

    private Vector3 originalPosition;
    private PauseMenu pauseMenu;

    void Start()
    {
        originalPosition = transform.position;
        pauseMenu = FindObjectOfType<PauseMenu>(); // Finds the PauseMenu in the scene
        if (pauseMenu == null)
        {
            Debug.LogError("PauseMenu not found in the scene!");
        }
    }

    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);

        float newY = originalPosition.y + Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.position = new Vector3(originalPosition.x, newY, originalPosition.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterController characterController = other.GetComponent<CharacterController>();
            if (characterController != null)
            {
                characterController.TryHeal(pauseMenu, healAmount, this.gameObject);
            }
        }
    }
}
