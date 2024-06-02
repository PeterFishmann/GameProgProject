
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordActivatorScript : MonoBehaviour
{
    public float rotationSpeed = 25f;
    public float bobHeight = 0.25f;
    public float bobSpeed = 1f;

    private Vector3 originalPosition;
    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);

        float newY = originalPosition.y + Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.position = new Vector3(originalPosition.x, newY, originalPosition.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered");
        if (other.CompareTag("Player"))
        {
            CharacterController characterController = other.GetComponent<CharacterController>();
            if (characterController != null)
            {
                characterController.ActivateSword();

            }
        }
    }

}
