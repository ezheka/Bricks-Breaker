using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;
using static SpawnBalls;

public class BlockConroller : MonoBehaviour
{
    public Canvas canvas;
    public TextMeshProUGUI textHP;

    public SpawnBlocks spawnBlocks;

    public int hp = 1;
    public string type = "spriteTriagle";

    private bool _isGotPoint = false;
    private int _countBlocks = 0;
    void Start()
    {
        canvas.worldCamera = Camera.main;
        
        float f = (Camera.main.pixelWidth / 2) / GetComponent<SpriteRenderer>().bounds.size.y;

        f = f / 10 / 2;
        
        textHP.GetComponent<RectTransform>().sizeDelta = new Vector2(f, f);
        textHP.fontSizeMax = f / 2;

        for(int i = 0; i < spawnBlocks.colorBlocks.Count; i++)
        {
            if (hp >= spawnBlocks.colorBlocks[i].countHP)
            {
                if(type == "spriteTriagle")
                    gameObject.GetComponent<SpriteRenderer>().sprite = spawnBlocks.colorBlocks[i].spriteTriagle;
                else
                    gameObject.GetComponent<SpriteRenderer>().sprite = spawnBlocks.colorBlocks[i].spriteBlock;
            }
        }
    }

    void Update()
    {
        textHP.text = hp.ToString();
        textHP.gameObject.transform.position = transform.position;

        if (hp <= 0)
        {

            if (!_isGotPoint && spawnBlocks.gameController.optionsGame != OptionsGame.BallInPlace)
            {
                spawnBlocks.gameController.effectsController.SpawnEffectDeleteBlock(transform.position);

                spawnBlocks.countPoints++;
                _countBlocks = spawnBlocks.countPoints;
                _isGotPoint = true;

                spawnBlocks.gameController.audioController.DeleteBlock();
            }
            spawnBlocks.gameController.score += 10 * _countBlocks;
            spawnBlocks.gameController.blockConrollers.Remove(gameObject.GetComponent<BlockConroller>());
            Destroy(gameObject);
        }


        float f = (Camera.main.pixelHeight / 2) / GetComponent<SpriteRenderer>().bounds.size.y;
        f = f / 10 / 2;

        textHP.GetComponent<RectTransform>().sizeDelta = new Vector2(f, f);
        textHP.fontSizeMax = f / 2;

        if (spawnBlocks.gameController.optionsGame == OptionsGame.BallInPlace)
        {
            _isGotPoint = false;
            _countBlocks = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /* Бонус МОЛНИЯ */
        if (collision.transform.tag == "ballLightminig")
        {
            spawnBlocks.gameController.effectsController.SpawnEffectElectricBlock(transform.position);
            hp = 0;
            spawnBlocks.gameController.spawnBalls.Spawn(true);
            spawnBlocks.gameController.optionsGame = OptionsGame.BallInPlace;
            Destroy(collision.gameObject);
            spawnBlocks.gameController.listBall.RemoveAt(0);



            spawnBlocks.gameController.spawnBalls._isSpawnLigthning = StateSpawn.WasSpawn;
            spawnBlocks.gameController.uiController.buttonBonusLightning.GetComponent<Button>().interactable = false;



            spawnBlocks.gameController.audioController.HitBlockLightning();

            /*foreach (BlockConroller block in spawnBlocks.gameController.blockConrollers)
            {
                block.Movement();
            }*/
        }

        /* Бонус Бомба */
        else if (collision.transform.tag == "ballBomb")
        {
            Vector2 vector = transform.position - collision.transform.position;

            if (vector.y > 0.2f)
            {
                Debug.LogWarning("Удар снизу");

                hp = 0;

                spawnBlocks.gameController.effectsController.SpawnEffectBombBlock(collision.transform.position);

                foreach (BlockConroller block in spawnBlocks.gameController.blockConrollers)
                {
                    if (block.transform.position.y <= transform.position.y - (spawnBlocks.step-.1f))
                    {
                        if (block.transform.position.x <= transform.position.x - (spawnBlocks.step - .1f) && block.transform.position.x >= transform.position.x - (spawnBlocks.step + .1f) ||
                            block.transform.position.x >= transform.position.x + (spawnBlocks.step - .1f) && block.transform.position.x <= transform.position.x + (spawnBlocks.step + .1f))
                        {
                            block.hp = 0;
                        }
                    }
                    if (block.transform.position.y == transform.position.y - (spawnBlocks.step*2) && block.transform.position.x == transform.position.x)
                    {
                        block.hp = 0;
                    }
                }
                Destroy(collision.gameObject);

            }
            else if (vector.y < -0.2f)
            {
                Debug.LogWarning("Удар сверху");

                hp = 0;

                foreach (BlockConroller block in spawnBlocks.gameController.blockConrollers)
                {
                    if (block.transform.position.y == transform.position.y + spawnBlocks.step)
                    {
                        if (block.transform.position.x <= transform.position.x - (spawnBlocks.step - .1f) && block.transform.position.x >= transform.position.x - (spawnBlocks.step + .1f) ||
                            block.transform.position.x >= transform.position.x + (spawnBlocks.step - .1f) && block.transform.position.x <= transform.position.x + (spawnBlocks.step + .1f))
                        {
                            block.hp = 0;
                        }
                    }
                    if (block.transform.position.y == transform.position.y + (spawnBlocks.step * 2) && block.transform.position.x == transform.position.x)
                    {
                        block.hp = 0;
                    }
                }
                Destroy(collision.gameObject);

            }
            else if (vector.x > 0.2f)
            {
                Debug.LogWarning("Удар справа");

                hp = 0;

                foreach (BlockConroller block in spawnBlocks.gameController.blockConrollers)
                {
                    if (block.transform.position.x == transform.position.x - spawnBlocks.step)
                    {
                        if (block.transform.position.y <= transform.position.y - (spawnBlocks.step - .1f) && block.transform.position.y >= transform.position.y - (spawnBlocks.step + .1f) ||
                            block.transform.position.y <= transform.position.y + (spawnBlocks.step - .1f) && block.transform.position.y >= transform.position.y + (spawnBlocks.step + .1f))
                        {
                            block.hp = 0;
                        }
                    }
                    if (block.transform.position.x == transform.position.x - (spawnBlocks.step * 2) && block.transform.position.y == transform.position.y)
                    {
                        block.hp = 0;
                    }
                }
                Destroy(collision.gameObject);

            }
            else if (vector.x < -0.2f)
            {
                Debug.LogWarning("Удар слева");

                hp = 0;

                foreach (BlockConroller block in spawnBlocks.gameController.blockConrollers)
                {
                    if (block.transform.position.x == transform.position.x + spawnBlocks.step)
                    {
                        if (block.transform.position.y <= transform.position.y - (spawnBlocks.step - .1f) && block.transform.position.y >= transform.position.y - (spawnBlocks.step + .1f) ||
                            block.transform.position.y <= transform.position.y + (spawnBlocks.step - .1f) && block.transform.position.y >= transform.position.y + (spawnBlocks.step + .1f))
                        {
                            block.hp = 0;
                        }
                    }
                    if (block.transform.position.x == transform.position.x + (spawnBlocks.step * 2) && block.transform.position.y == transform.position.y)
                    {
                        block.hp = 0;
                    }
                }
                Destroy(collision.gameObject);
            }


            spawnBlocks.gameController.audioController.HitBlockBomb();

            spawnBlocks.gameController.spawnBalls.Spawn(true);
            spawnBlocks.gameController.optionsGame = OptionsGame.BallInPlace;
            Destroy(collision.gameObject);
            spawnBlocks.gameController.listBall.RemoveAt(0);

            spawnBlocks.gameController.spawnBalls._isSpawnBomb = StateSpawn.WasSpawn;
            spawnBlocks.gameController.uiController.buttonBonusBomb.GetComponent<Button>().interactable = false;



        }

        /* ОБЫЧНЫЙ шар */
        else if (collision.transform.tag == "ball")
        {
            if (!collision.gameObject.GetComponent<BallController>().isMowing)
            {                
                hp--;

                GetComponent<ParticleSystem>().Play();

                spawnBlocks.gameController.audioController.HitBlock();

                for (int i = 0; i < spawnBlocks.colorBlocks.Count; i++)
                {
                    if (hp > spawnBlocks.colorBlocks[i].countHP)
                    {
                        if (type == "spriteTriagle")
                            gameObject.GetComponent<SpriteRenderer>().sprite = spawnBlocks.colorBlocks[i].spriteTriagle;
                        else
                            gameObject.GetComponent<SpriteRenderer>().sprite = spawnBlocks.colorBlocks[i].spriteBlock;
                        gameObject.GetComponent<ParticleSystem>().startColor = spawnBlocks.colorBlocks[i].colorParticle;
                    }
                }
            }
        }
    }

    public void Movement()
    {
        transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - spawnBlocks.step);

        foreach (BlockConroller block in spawnBlocks.gameController.blockConrollers)
        {
            if (block.transform.position.y <= spawnBlocks.gameController.lastPositionY)
            {
                block.GetComponent<Collider2D>().enabled = false;

                spawnBlocks.gameController.optionsGame = OptionsGame.GameOver;
                spawnBlocks.gameController.audioController.GameOver();
                spawnBlocks.gameController.uiController.ShowGamePanel();
                Debug.Log(spawnBlocks.gameController.optionsGame);
            }
        }
    }
}
