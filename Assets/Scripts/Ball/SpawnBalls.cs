using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;

public class SpawnBalls : MonoBehaviour
{
    [System.NonSerialized]
    public GameManager gameController;

    public StateSpawn _isSpawnLigthning = StateSpawn.DontSpawn;
    public StateSpawn _isSpawnBomb = StateSpawn.DontSpawn;
    public StateSpawn _isSpawnLine = StateSpawn.DontSpawn;
    public void Spawn(bool spawn)
    {
        if (spawn)
        {
            gameController.effectsController.SpawnEffectBall(gameController.startPositionBall);
        }


        Vector2 startVector = gameController.startPositionBall;

        for (int i = 1; i <= gameController.countBallsStart; i++)
        {
            if (gameController.listBall.Count <= gameController.countBallsStart)
            {
                var ball = Instantiate(gameController.ballPrefab, startVector, Quaternion.identity);
                ball.name = "ball_" + i;
                ball.transform.SetParent(transform);
                ball.GetComponent<BallController>().spawnBalls = this;

                ball.GetComponent<SpriteRenderer>().color = gameController.effectsController.collorBall;

                gameController.listBall.Add(ball);
            }
        }
    }

    private void Update()
    {
        if (transform.childCount > gameController.countBallsStart)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
    }

    public void BonusLightning()
    {
        if (_isSpawnLigthning == StateSpawn.DontSpawn && gameController.optionsGame == OptionsGame.BallInPlace)
        {
            Vector2 vector = gameController.listBall[0].transform.position;


            gameController.effectsController.SpawnEffectBallEllecttric(vector);

            var ball = Instantiate(gameController.ballPrefab, vector, Quaternion.identity);

            ball.transform.SetParent(transform);
            ball.GetComponent<BallController>().spawnBalls = this;

            ball.name = "ball_ligthihg";
            ball.tag = "ballLightminig";
            ball.GetComponent<SpriteRenderer>().color = gameController.effectsController.collorBallElectric;
            gameController.listBall.Add(ball);

            for (int i = 0; gameController.listBall.Count > 1; i++)
            {
                Destroy(gameController.listBall[0]);
                gameController.listBall.RemoveAt(0);
            }


            gameController.audioController.HitBlockLightning();
        }
    }

    public void BonusLine()
    {
        float deleteY = gameController.blockConrollers[0].transform.position.y;

        foreach (BlockConroller block in gameController.blockConrollers)
        {
            if (block.transform.position.y < deleteY && _isSpawnLine == StateSpawn.DontSpawn)
            {
                deleteY = block.transform.position.y;
                Debug.LogWarning(deleteY);
            }

        }
        _isSpawnLine = StateSpawn.WasSpawn;

        gameController.audioController.HitBlockLine(); 


        foreach (BlockConroller block in gameController.blockConrollers)
        {
            if (block.transform.position.y == deleteY)
            {
                block.hp = 0;

                gameController.effectsController.SpawnEffectDeleteBlock(block.transform.position);

                _isSpawnLine = StateSpawn.WasSpawn;
            }
        }


        gameController.uiController.buttonBonusLine.GetComponent<Button>().interactable = false;
    }

    public void BonusBomb()
    {
        if (_isSpawnBomb == StateSpawn.DontSpawn && gameController.optionsGame == OptionsGame.BallInPlace)
        {
            Vector2 vector = gameController.listBall[0].transform.position;


            gameController.effectsController.SpawnEffectBallBomb(vector);

            var ball = Instantiate(gameController.ballPrefab, vector, Quaternion.identity);

            ball.transform.SetParent(transform);
            ball.GetComponent<BallController>().spawnBalls = this;

            ball.name = "ball_bomb";
            ball.tag = "ballBomb";
            ball.GetComponent<SpriteRenderer>().color = gameController.effectsController.collorBallBomb;

            gameController.listBall.Add(ball);

            for (int i = 0; gameController.listBall.Count !=1 ; i++)
            {
                Destroy(gameController.listBall[0]);
                gameController.listBall.RemoveAt(0);
            }


            gameController.audioController.HitBlockBomb();
        }
    }

    public enum StateSpawn
    {
        DontSpawn,
        WasSpawn,
        MustNotSpawn
    }
}
