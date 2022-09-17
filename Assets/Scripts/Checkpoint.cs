using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Checkpoint : MonoBehaviour
{
    public int levelNumber;

    UIDocument ui;
    // Start is called before the first frame update
    void Start()
    {
        ui = GameObject.FindObjectOfType<UIDocument>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "BirbSprite")
        {
            var gameManager = GameObject.FindObjectOfType<GameManager>();
            if (gameManager.GetCurrentLevel() == levelNumber)
            {
                var levelCompleteLabel = ui.rootVisualElement.Query<Label>("Level-Complete-Text").AtIndex(0);
                levelCompleteLabel.text = "Level " + levelNumber.ToString() + " Complete";
                levelCompleteLabel.RemoveFromClassList("labelHidden");

                StartCoroutine(HideLevelCompleteLabel(levelCompleteLabel));

                gameManager.SetLevel(levelNumber++);
            }
        }
    }

    IEnumerator HideLevelCompleteLabel(Label levelCompleteLabel)
    {
        yield return new WaitForSeconds(3f);

        levelCompleteLabel.AddToClassList("labelHidden");
    }
}
