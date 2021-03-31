using System;
using System.Collections.Generic;

public class LevelStruct
{
    public Levels levels;
}

[Serializable]
public class Levels
{
    public List<Level> levels;
}

[Serializable]
public class Level
{
    public int countBalls;
    public Block[] blocks = new Block[99];
}

[Serializable]
public class Block
{
    public string typeBlock;
    public int hp;
}

public enum TypeBlock
{
    Standart,
    TiltLeftDown,
    TiltLeftUp,
    TiltRightDown,
    TiltRightUp,
}