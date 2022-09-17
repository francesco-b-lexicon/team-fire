using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeakCollider : MonoBehaviour

{
    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.LogWarning("trigenter" + col.gameObject.name);
        if (col.gameObject.tag == "destructable")
        {
            Destroy(col.gameObject);
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        Debug.LogWarning("trigstay" + col.gameObject.name);
        if (col.gameObject.tag == "destructable")
        {
            Destroy(col.gameObject);
        }
    }
}
