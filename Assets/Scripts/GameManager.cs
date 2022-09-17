using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int level = 0;
    public AudioSource backgroundMusic;

    void Start()
    {
        SetLevel(level);
    }

    public void SetLevel(int newLevel)
    {
        level = newLevel;
        backgroundMusic.GetComponent<BackgroundMusic>().SetClip(newLevel);
    }

    public int GetCurrentLevel()
    {
        return level;
    }
}
