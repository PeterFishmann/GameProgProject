using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HermesController : MonoBehaviour
{
    public float speedIncrease = 2f; 
    public float rotationSpeed = 50f; 
    public float bobHeight = 0.25f; 
    public float bobSpeed = 1f; 

    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.position;
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
                characterController.IncreaseSpeed(speedIncrease);
                Destroy(gameObject);
            }
        }
    }
}
