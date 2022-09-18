using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CutsceneTrigger : MonoBehaviour
{
    public VideoClip videoClip;
    private VideoPlayer vp;
    public bool startedPlaying = false;


    private GameObject mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        if (startedPlaying)
        {
            vp.loopPointReached += EndReached;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "BirbSprite" && !startedPlaying)
        {
            startedPlaying = true;
            vp = mainCamera.AddComponent<UnityEngine.Video.VideoPlayer>();
            vp.playOnAwake = false;
            vp.aspectRatio = VideoAspectRatio.FitOutside;
            vp.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;
            vp.clip = videoClip;
            vp.Play();
        }
    }

    private void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        Destroy(vp);
    }
}
