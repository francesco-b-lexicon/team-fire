using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FlightBarUI : MonoBehaviour
{
    public Transform TransformToFollow;

    private VisualElement _bar;
    private Camera _mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = Camera.main;
        _bar = GetComponent<UIDocument>().rootVisualElement.Q("#flightBar");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
