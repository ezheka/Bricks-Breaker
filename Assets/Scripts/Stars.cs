using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour
{
    public static int numberStar(int minCountBlocks, int countPoints)
    {
        if (countPoints >= minCountBlocks * 2.5f)
            return 3;
        
        else if (countPoints >= minCountBlocks * 1.8f)
            return 2;
        
        else if (countPoints >= minCountBlocks)
            return 1;
        
        else return 0;
    }
}
