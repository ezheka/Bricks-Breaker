using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class EffectsController : MonoBehaviour
{
    [Header("Эффект свечения шара")]
    public GameObject effectGlow;
    [Header("Цвета обычного шара")]
    public Color collorBall;
    [Header("Цвета Шара бомбы")]
    public Color collorBallBomb;
    [Header("Цвета Электричекого шара")]
    public Color collorBallElectric;

    [Header("Эффект обычного удара по блоку")]
    public GameObject effectHitBlock;

    [Header("Эффект уничтожения блока")]
    public GameObject effectDeleteBlock;

    [Header("Эффект удара по блоку бомбой")]
    public GameObject effectBombBlock;

    [Header("Эффект удара по блоку электричеством")]
    public GameObject effectElectricBlock;


    [Header("Эффект появления обычного шарика")]
    public GameObject effectSpawnBall;
    [Header("Эффект появления шарика-бомбы")]
    public GameObject effectSpawnBombBall;
    [Header("Эффект появления электрического шарика")]
    public GameObject effectSpawnElectricBall;


    [Header("Эффект появления звезды")]
    public GameObject effectSpawnStar;

    [System.NonSerialized]
    public GameManager gameController;
    public void SpawnEffectHitBlock(Vector2 position)
    {
        var effect = Instantiate(effectHitBlock, position, Quaternion.identity);
        effect.transform.SetParent(transform);
    }

    public void SpawnEffectBombBlock(Vector2 position)
    {
        var effect = Instantiate(effectBombBlock, position, Quaternion.identity);
        effect.transform.SetParent(transform);
    }

    public void SpawnEffectElectricBlock(Vector2 position)
    {
        var effect = Instantiate(effectElectricBlock, position, Quaternion.identity);
        effect.transform.SetParent(transform);
    }

    public void SpawnEffectDeleteBlock(Vector2 position)
    {
        var effect = Instantiate(effectDeleteBlock, position, Quaternion.identity);
        effect.transform.SetParent(transform);
    }

    public void SpawnEffectBall(Vector2 position)
    {
        var effect = Instantiate(effectSpawnBall, position, Quaternion.identity);
        effect.transform.SetParent(transform);
    }

    public void SpawnEffectBallBomb(Vector2 position)
    {
        var effect = Instantiate(effectSpawnBombBall, position, Quaternion.identity);
        effect.transform.SetParent(transform);
    }

    public void SpawnEffectBallEllecttric(Vector2 position)
    {
        var effect = Instantiate(effectSpawnElectricBall, position, Quaternion.identity);
        effect.transform.SetParent(transform);
    }

    public void SpawnEffectStar(Vector2 position)
    {
        var effect = Instantiate(effectSpawnStar, position, Quaternion.identity);
        effect.transform.SetParent(transform);
    }

    private void Update()
    {
        if (gameController.optionsGame == OptionsGame.BallInPlace)
        {
            effectGlow.SetActive(true);
            effectGlow.transform.position = gameController.listBall[0].transform.position;
            effectGlow.GetComponent<ParticleSystem>().startColor = gameController.listBall[0].GetComponent<SpriteRenderer>().color;
        }
        else
            effectGlow.SetActive(false);
    }
}
