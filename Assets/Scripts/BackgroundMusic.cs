using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BackgroundMusic : MonoBehaviour
{
    public AudioClip[] music;
    private AudioSource player;
    // Start is called before the first frame update
    void Awake()
    {
        player = GetComponent<AudioSource>();
    }

    public void SetClip(int level)
    {
        player.clip = music[level];
    }
}
