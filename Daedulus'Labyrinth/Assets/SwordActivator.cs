using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordActivatorScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered");
        if (other.CompareTag("Player"))
        {
            CharacterController characterController = other.GetComponent<CharacterController>();
            if (characterController != null)
            {
                Debug.Log("Activating sword for the player");
                characterController.ActivateSword();
            }
        }
    }
}
