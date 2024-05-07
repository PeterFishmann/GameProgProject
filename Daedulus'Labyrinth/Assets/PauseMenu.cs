using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] Text numberText; 
    int currentNumber = 0; 
    int targetNumber = 5; 
    float updateInterval = 5f; 
    float timer = 0f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.activeSelf)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        if (currentNumber < targetNumber)
        {
            timer += Time.deltaTime;
            if (timer >= updateInterval)
            {
                currentNumber++;
                UpdateNumberText();
                timer = 0f;
            }
        }
    }

    void UpdateNumberText()
    {
        numberText.text = currentNumber.ToString();
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Home()
    {
        SceneManager.LoadScene(0);
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
