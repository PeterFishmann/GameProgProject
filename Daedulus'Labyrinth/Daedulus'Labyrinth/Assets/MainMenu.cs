using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Reference to the GameObject you want to rotate
    public GameObject rotatingObject;
    // Speed of rotation
    public float rotationSpeed = 10f;

    void Update()
    {
        // Rotate the GameObject counterclockwise around its center
        rotatingObject.transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
        rotatingObject.transform.Rotate(Vector3.forward, -rotationSpeed * Time.deltaTime);
        rotatingObject.transform.Rotate(Vector3.right, -rotationSpeed * Time.deltaTime);
    }

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
