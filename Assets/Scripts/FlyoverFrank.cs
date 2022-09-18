using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyoverFrank : MonoBehaviour
{
    // Update is called once per frame
    void Awake()
    {
        StartCoroutine(DestroyFrank());
    }

    private IEnumerator DestroyFrank()
    {
        yield return new WaitForSeconds(9);

        Destroy(gameObject);
    }
}
