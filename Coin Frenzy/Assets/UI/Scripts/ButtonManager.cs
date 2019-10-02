using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public AudioClip clickSound;
    public bool pauseClick;

    private AudioSource cameraAudioSource;
    private void Start()
    {
        cameraAudioSource = Camera.main.GetComponent<AudioSource>();
    }

    public void StartGameClick()
    {
        PlayClickSound();
        SceneManager.LoadScene("Main");
    }

    public void PauseClick()
    {
        PlayClickSound();
        pauseClick = true;
    }

    public void QuitClick()
    {
        Application.Quit();
    }

    public void PlayClickSound()
    {
        cameraAudioSource.PlayOneShot(clickSound, 1.0f);
    }
}
