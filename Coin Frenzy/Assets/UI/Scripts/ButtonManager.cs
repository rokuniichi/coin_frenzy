using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public bool pauseClick;
    public void StartGameClick()
    {
        SceneManager.LoadScene("Main");
    }

    public void PauseClick()
    {
        pauseClick = true;
    }
}
