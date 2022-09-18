using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CutsceneTrigger : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public VideoClip videoClip;
    private VideoPlayer vp;
    private bool startedPlaying = false;


    private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (startedPlaying)
        {
            var isPlaying = vp.isPlaying;

            if (!isPlaying)
            {
                Destroy(vp);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "BirbSprite" && !startedPlaying)
        {
            startedPlaying = true;
            vp = Instantiate(videoPlayer);
            vp.transform.parent = mainCamera.transform;
            vp.clip = videoClip;
            vp.Play();
        }
    }
}
