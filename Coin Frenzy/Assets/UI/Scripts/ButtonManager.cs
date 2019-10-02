using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public AudioClip clickSound;

    private GameManager gm;
    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    public bool pauseClick;
    public void StartGameClick()
    {
        gm.playSound(clickSound);
        SceneManager.LoadScene("Main");
    }

    public void PauseClick()
    {
        gm.playSound(clickSound);
        pauseClick = true;
    }

    public void QuitClick()
    {
        Application.Quit();
    }
}
