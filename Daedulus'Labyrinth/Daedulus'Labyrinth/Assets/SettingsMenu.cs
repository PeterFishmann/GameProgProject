using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
    }

    public void BackButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void volumeAdjust()
    {

    }


    //public void PauseGame()
    //{
    //    ButtonList.setActive(true);
    //    Time.timeScale = 0f;
    //    isPaused = true;
    //}

    //public void ResumeGame()
    //{
    //    ButtonList.setActive(false);
    //    Time.timeScale = 1f;
    //    isPaused = false;
    //}


}
