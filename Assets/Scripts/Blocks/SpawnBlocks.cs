using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class SpawnBlocks : MonoBehaviour
{
    public List<ColorBLock> colorBlocks;
    public GameObject leftBorder, rightBorder, leftBorder2, rightBorder2, bottomBorder;

    [System.NonSerialized]
    public GameManager gameController;

    public float startX = -2f, finishX = 2f,
                 startY = 3f, finishY = -2f;

    public int countPoints;

    [System.NonSerialized]
    public float step;
    private float wight, wightBlock;

    private void Awake()
    {

        Vector2 bottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        Vector2 bottomRight = Camera.main.ScreenToWorldPoint(new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight));

        leftBorder.transform.position = new Vector2(bottomLeft.x, 0);
        rightBorder.transform.position = new Vector2(bottomRight.x, 0);
        leftBorder2.transform.position = new Vector2(bottomLeft.x, bottomRight.y);
        rightBorder2.transform.position = new Vector2(bottomRight.x, bottomRight.y);

        wight = bottomRight.x * 2;        
        step = wight / 9;        
        wightBlock = step + (step / 2);

        startX = bottomLeft.x + (step / 2);
        finishX = bottomRight.x - (step / 2);

        Debug.LogWarning(bottomRight + "**" + wight + "**" + step + "**" + wightBlock);

    }

    public void Spawn()
    {
        gameController.lastPositionY = finishY - (step/2);
        gameController.startPositionBall = new Vector2(0, finishY - (step * 2)+.3f);
        bottomBorder.transform.position = new Vector2(0, finishY - (step * 2));

        int numberLevel = PlayerPrefs.GetInt("NumberLevel", 1);

        int _intX = -1;
        for (float y = startY; y > finishY+.2f; y -= step)
        {
            for (float x = startX; x < finishX+.2f; x += step)
            {
                _intX++;

                GameObject block;
                string typeBlock = gameController.saveSerial.levels.levels[numberLevel - 1].blocks[_intX].typeBlock;
                if (typeBlock == "TiltLeftDown")
                {
                    block = Instantiate(gameController.blockPrefabTLD, new Vector2(x, y), Quaternion.identity);
                }
                else if (typeBlock == "TiltLeftUp")
                {
                    block = Instantiate(gameController.blockPrefabTLU, new Vector2(x, y), Quaternion.identity);
                }
                else if (typeBlock == "TiltRightDown")
                {
                    block = Instantiate(gameController.blockPrefabTRD, new Vector2(x, y), Quaternion.identity);
                }
                else if (typeBlock == "TiltRightUp")
                {
                    block = Instantiate(gameController.blockPrefabTRU, new Vector2(x, y), Quaternion.identity);
                }
                else
                {
                    block = Instantiate(gameController.blockPrefabStandart, new Vector2(x, y), Quaternion.identity);
                    block.GetComponent<BlockConroller>().type = "spriteBlock";
                }

                block.transform.SetParent(transform, true);


                block.GetComponent<BlockConroller>().hp = gameController.saveSerial.levels.levels[numberLevel-1].blocks[_intX].hp;
                //block.GetComponent<BlockConroller>().hp = 5;

                if (block.GetComponent<BlockConroller>().hp > 0)
                {
                    gameController.minCountPoints += 10;
                }

                block.GetComponent<Transform>().localScale = new Vector2(wightBlock, wightBlock);
                                
                block.GetComponent<BlockConroller>().spawnBlocks = this;

                gameController.blockConrollers.Add(block.GetComponent<BlockConroller>());
            }
        }
    }

    [Serializable]
    public class ColorBLock
    {
        public int countHP;
        public Sprite spriteBlock;
        public Sprite spriteTriagle;
        public Color  colorParticle;
    }

    private void Update()
    {
        if (gameController.optionsGame == OptionsGame.BallInPlace)
            countPoints = 0;
    }
}
