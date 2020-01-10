using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticGameInformation
{
    private static string levelNameToLoad = "Level1_Ice_Tutorial";

    public static string LevelNameToLoad
    {
        get { return levelNameToLoad; }
        set { levelNameToLoad = value; }
    }
}
