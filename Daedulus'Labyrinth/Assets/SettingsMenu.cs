
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public Slider MusicSlider;
    public Slider SFXSlider;
    public AudioMixer audioMixer;
    // Start is called before the first frame update
    void Start()
    {
        MusicSlider.onValueChanged.AddListener(SetVolume);
        SFXSlider.onValueChanged.AddListener(SetSFX);

        float volume = 0f;
        float sfxVolume = 0f;
        audioMixer.GetFloat("Master Volume", out volume);
        audioMixer.GetFloat("SFX Volume", out sfxVolume);

        MusicSlider.value = Mathf.Pow(10, volume / 20); // Convert from dB to linear value
        SFXSlider.value = Mathf.Pow(10,sfxVolume /20);
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

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Master Volume", Mathf.Log10(volume) * 20); // Convert to dB

    }
    public void SetSFX(float sfxVolume)
    {
        audioMixer.SetFloat("SFX Volume", Mathf.Log10(sfxVolume) * 20); // Logarithmic scale for audio

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
