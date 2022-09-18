using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Checkpoint : MonoBehaviour
{
    public int levelNumber;
    private GameManager gameManager;

    void Start()
    {
        gameManager = GetComponentInParent<GameManager>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "BirbSprite")
        {
            gameManager.SetLevel(levelNumber);
        }
    }


}
