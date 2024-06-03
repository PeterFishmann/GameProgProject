using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateController : MonoBehaviour
{
    public float rotationSpeed = 25f;
    public float bobHeight = 0.25f;
    public float bobSpeed = 1f;

    private Vector3 originalPosition;

    void Start()
    {
        // Initialize the original position of the GameObject
        originalPosition = transform.position;
    }

    void Update()
    {
        // Rotate the GameObject around the Y axis and create a bobbing motion
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);

        // Calculate and apply the bobbing motion to the GameObject's position
        float newY = originalPosition.y + Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.position = new Vector3(originalPosition.x, newY, originalPosition.z);
    }
}
