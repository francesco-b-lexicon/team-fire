using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int level = 0;
    private BackgroundMusic backgroundMusic;

    void Start()
    {
        backgroundMusic = GetComponentInChildren<BackgroundMusic>();
        SetLevel(level);
    }

    public void SetLevel(int newLevel)
    {
        level = newLevel;
        backgroundMusic.SetClip(newLevel);
    }

    public int GetCurrentLevel()
    {
        return level;
    }
}
