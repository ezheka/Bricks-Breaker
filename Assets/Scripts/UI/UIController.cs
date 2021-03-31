using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static GameManager;

public class UIController : MonoBehaviour
{
    [System.NonSerialized]
    public GameManager gameController;

    [Header("Игровые тексты")]
    public TextMeshProUGUI numberLevelText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI countBallsText;
    [Header("Кнопка сбора шаров")]
    public GameObject buttonCollectBalls;

    [Header("Игровые панели")]
    public GameObject gamePanel;
    public GameObject pausePanel;
    public GameObject buttonNextLevel;
    public GameObject buttonRestartLevel;
    public TextMeshProUGUI titlePanelText;


    [Header("Игровые кнопки")]
    public GameObject buttonBonusLightning;
    public GameObject buttonBonusLine;
    public GameObject buttonBonusBomb;

    [Header("Настройка звука")]
    public AudioListener audioListener;
    public Image soundButton;
    public Sprite soundOn, soundOff;


    public GameObject panel_1, panel_2;

    public Image star_1, star_2, star_3;
    public Image starPanel_1, starPanel_2, starPanel_3;
    public Sprite star;

    private bool _isPause;

    void Start()
    {
        gamePanel.SetActive(false);
        pausePanel.SetActive(false); 
        Time.timeScale = 1;
        _isPause = false;

        numberLevelText.text = PlayerPrefs.GetInt("NumberLevel", 1)+" ур";

        if (PlayerPrefs.GetString("Sound", "on") == "on")
        {
            audioListener.enabled = true;
            soundButton.sprite = soundOn;
        }
        else
        {
            audioListener.enabled = false;
            soundButton.sprite = soundOff;
        }
    }

    void Update()
    {
        scoreText.text = gameController.score.ToString();
        countBallsText.text = "x" + gameController.countBallsStart.ToString();


        if (gameController.optionsGame == OptionsGame.BallInPlace)
        {
            panel_1.SetActive(true); panel_2.SetActive(false);
        }
        if (gameController.optionsGame == OptionsGame.BallFlying)
        {
            panel_1.SetActive(false); panel_2.SetActive(true);
        }

        int countStar = Stars.numberStar(gameController.minCountPoints, gameController.score);

        if(countStar == 1 && star_1.sprite != star)
        {
            star_1.sprite = star;
            gameController.effectsController.SpawnEffectStar(star_1.transform.position);
            gameController.audioController.AudioStar();
        }
        if (countStar == 2 && star_2.sprite != star)
        {
            star_2.sprite = star;
            gameController.effectsController.SpawnEffectStar(star_2.transform.position);
            gameController.audioController.AudioStar();
        }
        if (countStar == 3 && star_3.sprite != star)
        {
            star_3.sprite = star;
            gameController.effectsController.SpawnEffectStar(star_3.transform.position);
            gameController.audioController.AudioStar();
        }

        if(gameController.optionsGame == OptionsGame.GameWin)
        {
            if (countStar == 1 && starPanel_1.sprite != star)
            {
                starPanel_1.sprite = star;
                gameController.effectsController.SpawnEffectStar(starPanel_1.transform.position);

                gameController.audioController.AudioStarMenu(false);
            }
            if (countStar == 2 && starPanel_2.sprite != star)
            {
                starPanel_1.sprite = star;
                gameController.effectsController.SpawnEffectStar(starPanel_1.transform.position);
                starPanel_2.sprite = star;
                gameController.effectsController.SpawnEffectStar(starPanel_2.transform.position);

                gameController.audioController.AudioStarMenu(false);
            }
            if (countStar == 3 && starPanel_3.sprite != star)
            {
                starPanel_1.sprite = star;
                gameController.effectsController.SpawnEffectStar(starPanel_1.transform.position);
                starPanel_2.sprite = star;
                gameController.effectsController.SpawnEffectStar(starPanel_2.transform.position);
                starPanel_3.sprite = star;
                gameController.effectsController.SpawnEffectStar(starPanel_3.transform.position);


                gameController.audioController.AudioStarMenu(true);
            }
        }
    }

    public void Pause()
    {
        _isPause = !_isPause;
        pausePanel.SetActive(_isPause);
        if (_isPause)
            Time.timeScale = 0;
        else Time.timeScale = 1;
    }


    public void BonusLightning()
    {
        gameController.spawnBalls.BonusLightning();
    }

    public void BonusLine()
    {
        gameController.spawnBalls.BonusLine();
    }

    public void BonusBomb()
    {
        gameController.spawnBalls.BonusBomb();
    }


    public void ShowGamePanel()
    {
        if (gameController.optionsGame == OptionsGame.GameOver)
        {
            titlePanelText.text = "Пропробуй ещё!";
            buttonNextLevel.SetActive(false);
            buttonRestartLevel.SetActive(true);
        }
        if (gameController.optionsGame == OptionsGame.GameWin)
        {
            CollectBalls();
            int curentLevel = PlayerPrefs.GetInt("NumberLevel", 1);

            PlayerPrefs.SetInt(curentLevel.ToString(), 1);

            if (curentLevel == gameController.saveSerial.levels.levels.Count)
            {
                titlePanelText.text = "Успешно!";
                buttonNextLevel.SetActive(false);
                buttonRestartLevel.SetActive(true);
            }
            else
            {
                titlePanelText.text = "Успешно!";
                buttonNextLevel.SetActive(true);
                buttonRestartLevel.SetActive(false);
            }
        }

        gamePanel.SetActive(true);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void nextLevelStart()
    {
        if (gameController.optionsGame == OptionsGame.GameWin)
        {
            gameController.SaveProgress(PlayerPrefs.GetInt("NumberLevel", 1));
        }

        RestartScene();
    }

    public void GoMain()
    {
        SceneManager.LoadScene(0);
    }

    public void CollectBalls()
    {
        if (gameController.tempListBall.Count > 0 && gameController._isAiming)
        {
            gameController.optionsGame = OptionsGame.BallInPlace;

            Vector2 vector = gameController.tempListBall[0].transform.position;

            gameController._isAiming = false;
            gameController.tempListBall.Clear();

            foreach (GameObject ball in gameController.listBall)
            {
                ball.GetComponent<BallController>().StopAllCoroutines();
                ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                ball.GetComponent<BallController>().vectorMoving = vector;
                ball.GetComponent<BallController>().isMowing = true;

                gameController.tempListBall.Add(ball);
            }
        }
        Debug.Log("Stop");

    }

    public void ChangingSound()
    {
        if (PlayerPrefs.GetString("Sound") == "on")
        {
            audioListener.enabled = false;
            PlayerPrefs.SetString("Sound", "off");
            soundButton.sprite = soundOff;
        }
        else
        {
            audioListener.enabled = true;
            PlayerPrefs.SetString("Sound", "on");
            soundButton.sprite = soundOn;
        }
    }
}
