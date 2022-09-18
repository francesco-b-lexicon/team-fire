using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyoverFrank : MonoBehaviour
{

    public float speed = 1f;
    public float height = 5f;
    private float endPos = 0;

    // Update is called once per frame
    void Start()
    {
        endPos = transform.position.x + 100;
    }
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed);
        if (transform.position.x > endPos)
        {
            Destroy(gameObject);
        }
    }
}
