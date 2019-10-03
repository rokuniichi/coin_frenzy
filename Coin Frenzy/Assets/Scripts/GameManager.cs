using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private const float GAME_TIME = 60.0f;

    [HideInInspector]
    public bool gameOver;

    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text timeText;
    [SerializeField]
    private Text gameOverText;
   
    public AudioClip coinSound;
    public AudioClip victorySound;
    public AudioClip gameOverSound;
    public AudioClip powerUpSound;

    private AudioSource cameraAudioSource;
    private AudioSource playerAudioSource;
    private GameObject[] gameUI;
    private GameObject[] pauseUI;
    private GameObject[] hideGameOverUI;
    private GameObject[] showGameOverUI;
    private ButtonManager bm;
    private PlayerController controller;
    private int coinsCollected;
    private float timer;
    private Rect tempRect;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        gameUI = GameObject.FindGameObjectsWithTag("GameUI");
        pauseUI = GameObject.FindGameObjectsWithTag("PauseUI");
        hideGameOverUI = GameObject.FindGameObjectsWithTag("HideGameOverUI");
        showGameOverUI = GameObject.FindGameObjectsWithTag("ShowGameOverUI");
        bm = GameObject.FindGameObjectWithTag("Canvas").GetComponent<ButtonManager>();
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        cameraAudioSource = Camera.main.GetComponent<AudioSource>();
        playerAudioSource = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
        SetActiveUI(gameUI, true);
        SetActiveUI(pauseUI, false);
        SetActiveUI(showGameOverUI, false);
        coinsCollected = 0;
        scoreText.text = "Coins: " + coinsCollected;
        timer = GAME_TIME;
    }

    // Update is called once per frame
    void Update()
    {
        TimerHandler();
        if (gameOver)
        {
            GameOverHandler();
        }
        if (bm.pauseClick)
        {
            PauseHandler();
        }
    }

    public void CoinCollectHandler()
    {
        coinsCollected++;
        scoreText.text = "Coins: " + coinsCollected;
    }

    private void TimerHandler()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            timeText.text = "Time: " + Mathf.RoundToInt(timer) + "s";
            if (timer < 0)
            {
                gameOver = true;
                timer = 0;
            }
        }
        
    }

    private void SetActiveUI(GameObject[] objectsUI, bool show)
    {
        foreach (GameObject obj in objectsUI)
        {
            obj.SetActive(show);
        }
    }

    public void PlaySoundOnCamera(AudioClip sound)
    {
        cameraAudioSource.PlayOneShot(sound, 1.0f);
    }

    public void PlaySoundOnPlayer(AudioClip sound)
    {
        playerAudioSource.PlayOneShot(sound, 1.0f);
    }
    private void GameOverHandler()
    {
        gameOver = false;
        controller.StopAllCoroutines();
        Time.timeScale = 0;
        gameOverText.color = Color.red;
        SetActiveUI(gameUI, false);
        SetActiveUI(hideGameOverUI, false);
        SetActiveUI(pauseUI, true);
        SetActiveUI(showGameOverUI, true);

        PlaySoundOnCamera(gameOverSound);

        //tempRect = scoreTextRt.rect;
        //scoreTextRt.localPosition = new Vector3(-SCORE_TEXT_OFFSET, SCORE_TEXT_OFFSET, 0);
    }
    public void ExitHandler()
    {
        controller.StopAllCoroutines();
        Time.timeScale = 0;
        gameOverText.text = "VICTORY!\n" + "Coins: " + coinsCollected;
        gameOverText.color = Color.green;
        SetActiveUI(gameUI, false);
        SetActiveUI(hideGameOverUI, false);
        SetActiveUI(pauseUI, true);
        SetActiveUI(showGameOverUI, true);

        PlaySoundOnCamera(victorySound);

        //tempRect = scoreTextRt.rect;
        //scoreTextRt.localPosition = new Vector3(-SCORE_TEXT_OFFSET, SCORE_TEXT_OFFSET, 0);
    }

    public void PauseHandler()
    {
        bm.pauseClick = false;
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            SetActiveUI(gameUI, false);
            SetActiveUI(pauseUI, true);

        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            SetActiveUI(pauseUI, false);
            SetActiveUI(gameUI, true);
        }
    }

}
