using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMapManager : MonoBehaviour
{
    [System.Serializable]
    public struct Leveldata
    {
        public int level;
        public Sprite mapSprite;
    }
    public Leveldata[] levels;
    public int currentLevel = 1;

    public static int levelshit;

    private void Start()
    {
        currentLevel = Mathf.Clamp(currentLevel, 1, 33);
        Sprite currentSprite = FindCurrentLevelSprite(currentLevel);

        GetComponent<SpriteRenderer>().sprite = currentSprite;

        levelshit = currentLevel;
    }

    public Sprite FindCurrentLevelSprite(int level)
    {
        foreach(var lvl in levels)
        {
            if(level == lvl.level)
            {
                return lvl.mapSprite;
            }
        }
        return null;
    }
}
