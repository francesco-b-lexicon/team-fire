using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
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
            if (Input.GetKey(KeyCode.F))
            {
                EndReached(vp);
            }
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
            var ui = GameObject.FindObjectOfType<UIDocument>();
            ui.rootVisualElement.AddToClassList("hidden");
            vp.Play();
        }
    }

    private void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        Destroy(vp);
        var gameManager = GameObject.FindObjectOfType<GameManager>();
        gameManager.SetVolume(0.6f);
        var ui = GameObject.FindObjectOfType<UIDocument>();
        ui.rootVisualElement.RemoveFromClassList("hidden");
    }
}
