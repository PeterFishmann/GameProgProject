using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HermesController : MonoBehaviour
{
    public float speedIncrease = 2f;
    private PauseMenu pauseMenu;

    void Start()
    {
        pauseMenu = FindObjectOfType<PauseMenu>();
        if (pauseMenu == null)
        {
            Debug.LogError("PauseMenu not found in the scene!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterController characterController = other.GetComponent<CharacterController>();
            if (characterController != null)
            {
                characterController.TryIncreaseSpeed(pauseMenu, speedIncrease, this.gameObject);
            }
        }
    }
}
