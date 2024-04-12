using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
/*    [SerializeField] private AudioMixer myMixed;
    [SerializeField] private Slider musicSlider;

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        myMixed.SetFloat("music", Mathf.Log10(volume) * 20);
    }*/


    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
