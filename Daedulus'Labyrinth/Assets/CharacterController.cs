using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CharacterController : MonoBehaviour
{
    public float moveSpeed = 5f; // speed of movement
    public bool isPaused;
    public GameObject ButtonList;

    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation; // Freeze rotation
        ButtonList.SetActive(false);
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
        if (Input.GetKeyDown(KeyCode.E)) {
            //Interact();
        }
        if (Input.GetMouseButtonDown(0)){
            //Attack();
        }
        if (Input.GetMouseButtonDown(1)){
            //Dodge();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //if (isPaused)
            // {
            //     ResumeGame();
            // }
            // else
            //  {
            //      PauseGame();
            //  }

            SceneManager.LoadScene("MainMenu");
        }
    void PauseGame(){
        isPaused = true;
        Time.timeScale = 0f; // Pause the game
        ButtonList.SetActive(true); 
    }

    //public void ResumeGame(){
    //    isPaused = false;
    //    Time.timeScale = 1f; // Resume normal time scale
    //    ButtonList.SetActive(false);
    //}

        transform.Translate(movement * moveSpeed * Time.deltaTime);

        
    }

}
