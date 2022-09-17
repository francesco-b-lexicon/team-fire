using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Checkpoint : MonoBehaviour
{
    public string levelName;
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
            var levelCompleteLabel = ui.rootVisualElement.Query<Label>("Level-Complete-Text").AtIndex(0);
            levelCompleteLabel.text = levelName;
            levelCompleteLabel.RemoveFromClassList("labelHidden");

            StartCoroutine(HideLevelCompleteLabel(levelCompleteLabel));
        }
    }

    IEnumerator HideLevelCompleteLabel(Label levelCompleteLabel)
    {
        yield return new WaitForSeconds(3f);

        levelCompleteLabel.AddToClassList("labelHidden");
    }
}
