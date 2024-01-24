using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    [Header("Menus")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject options;
    [SerializeField] private GameObject controls;

    [Header("Audio")]
    [SerializeField] private AudioMixer musicAudioMixer;
    [SerializeField] private AudioMixer soundEffectsAudioMixer;

    private string timerPrefsName = "Timer";
    private string levelPrefsName = "Level";

    public void ChangeGameScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void EnterOptions()
    {
        mainMenu.SetActive(false);
        options.SetActive(true);
    }

    public void MusicControler(float sliderMusica)
    {
        musicAudioMixer.SetFloat("MusicVolume", Mathf.Log10(sliderMusica) * 20);
    }

    public void SoundEffectsControler(float sliderSoundEffects)
    {
        soundEffectsAudioMixer.SetFloat("SoundEffectsVolume", Mathf.Log10(sliderSoundEffects) * 20);
    }

    public void ExitOptions()
    {
        options.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void EnterControls()
    {
        mainMenu.SetActive(false);
        controls.SetActive(true);
    }

    public void ExitControls()
    {
        controls.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }

    private void SaveData()
    {
        PlayerPrefs.SetFloat(timerPrefsName, 0);
        PlayerPrefs.SetFloat(levelPrefsName, 3);
    }

    private void OnDestroy()
    {
        SaveData();
    }
}
