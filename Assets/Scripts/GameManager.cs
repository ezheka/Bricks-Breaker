using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public SaveSerial saveSerial;
    
    [Header("Настройка шариков")]
    public SpawnBalls spawnBalls;
    public GameObject SpriteLine;
    [Header("Префаб шарика")]
    public GameObject ballPrefab;
    [Header("Скорость шариков")]
    public float speedBalls;
    [Header("Количество шариков")]
    public int countBallsStart;
    [Header("Начальные координаты шарика")]
    public Vector2 startPositionBall;

    [Header("Настройка блоков")]
    public SpawnBlocks spawnBlocks;
    [Header("Префабы блоков")]
    public GameObject blockPrefabStandart;
    public GameObject blockPrefabTLD;
    public GameObject blockPrefabTLU;
    public GameObject blockPrefabTRD;
    public GameObject blockPrefabTRU;
    [Header("Последняя координата по Y")]
    public float lastPositionY;
    
    [Header("Контроллер UI")]
    public UIController uiController;

    [Header("Контроллер Эффектов")]
    public EffectsController effectsController;

    [Header("Контроллер Звуков")]
    public AudioController audioController;

    [Header("Слайдер звёзд")]
    public Slider sliderStar;


    public List<GameObject> listBall, tempListBall;
    public List<BlockConroller> blockConrollers;

    [System.NonSerialized]
    public OptionsGame optionsGame;
    [System.NonSerialized]
    public int score = 0, countBalls, minCountPoints;
    [System.NonSerialized]
    public bool _isAiming;

    private void Awake()
    {
        _isAiming = false;

        optionsGame = OptionsGame.BallInPlace;

        spawnBalls.gameController = this;
        spawnBlocks.gameController = this;
        uiController.gameController = this;
        effectsController.gameController = this;

        countBalls = countBallsStart;
    }

    private void Start()
    {
        int numberLevel = PlayerPrefs.GetInt("NumberLevel", 1);
        countBallsStart = saveSerial.levels.levels[numberLevel-1].countBalls;
        spawnBlocks.Spawn();
        spawnBalls.Spawn(true);
    }

    private void Update()
    {
        if (listBall.Count == tempListBall.Count)
        {
            StopAllCoroutines();

            // Сдвиг блоков на одну клетку вниз
            foreach (BlockConroller block in blockConrollers)
            {
                block.Movement();
            }

            countBalls = countBallsStart;
            tempListBall.Clear();
            optionsGame = OptionsGame.BallInPlace;
        }

        if (blockConrollers.Count == 0 && optionsGame != OptionsGame.GameWin)
        {
            optionsGame = OptionsGame.GameWin;
            uiController.ShowGamePanel();
            Debug.Log(optionsGame);
        }

        sliderStar.maxValue = minCountPoints * 2.5f;
        sliderStar.value = score;
        int countStar = Stars.numberStar(minCountPoints, score);

    }

    public IEnumerator ThrowingBall()
    {
        foreach (GameObject ball in listBall)
        {
            if (_isAiming)
                ball.GetComponent<BallController>().Send();
            yield return new WaitForSeconds(.05f);
        }
    }

    [System.Serializable]
    public enum OptionsGame
    {
        BallFlying,
        BallInPlace,
        GameWin,
        GameOver
    }


    public void LoadProgress()
    {
        //int numberLevel = PlayerPrefs.GetInt("NumberLevel", 1);
        //countBallsStart = saveSerial.levels.levels[0].countBalls;
    }

    public void SaveProgress(int numberLevel)
    {
        if (numberLevel < saveSerial.levels.levels.Count) numberLevel++;

        PlayerPrefs.SetInt("NumberLevel", numberLevel);
    }
}