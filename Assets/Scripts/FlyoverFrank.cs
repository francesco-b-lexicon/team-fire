using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyoverFrank : MonoBehaviour
{

    public float speed = 1f;
    private float endPos = 0;

    // Update is called once per frame
    void Start()
    {
        endPos = transform.position.x + 100;
    }
    void Update()
    {
        transform.Translate(new Vector3(1 * Time.deltaTime * speed, 0, 0));
        if (transform.position.x > endPos)
        {
            Destroy(gameObject);
        }
    }
}
