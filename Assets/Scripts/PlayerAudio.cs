using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public AudioClip attackSound;
    public AudioClip walkingCitySound;
    public AudioClip walkingSandSound;
    public AudioClip stumbleSound;
    public AudioClip wingSound;
    private AudioSource audioPlayer;


    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
    }

    public void PlayAttackSound()
    {
        audioPlayer.Stop();
        audioPlayer.PlayOneShot(attackSound);
    }

    public void PlayWalkingSound()
    {
        if (!audioPlayer.isPlaying)
        {
            audioPlayer.PlayOneShot(walkingCitySound);
        }
    }

    public void PlayFlyingSound()
    {
        if (!audioPlayer.isPlaying)
        {
            audioPlayer.PlayOneShot(wingSound);
        }
    }

    public void PlayStumbleSound()
    {
        audioPlayer.Stop();
        audioPlayer.PlayOneShot(stumbleSound);
    }


}
