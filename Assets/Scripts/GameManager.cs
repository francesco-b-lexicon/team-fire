using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    private int level = 0;
    private BackgroundMusic backgroundMusic;

    private UIDocument ui;
    // Start is called before the first frame update

    void Start()
    {
        ui = FindObjectOfType<UIDocument>();
        backgroundMusic = GetComponentInChildren<BackgroundMusic>();
        SetLevel(1);
    }

    public void SetLevel(int newLevel)
    {
        if (level != newLevel)
        {
            var levelCompleteLabel = ui.rootVisualElement.Query<Label>("Level-Complete-Text").AtIndex(0);
            levelCompleteLabel.text = "Level " + level.ToString() + " Complete";
            levelCompleteLabel.RemoveFromClassList("labelHidden");

            StartCoroutine(HideLevelCompleteLabel(levelCompleteLabel));

            level = newLevel;
            backgroundMusic.SetClip(newLevel - 1);
        }
    }
    IEnumerator HideLevelCompleteLabel(Label levelCompleteLabel)
    {
        yield return new WaitForSeconds(3f);

        levelCompleteLabel.AddToClassList("labelHidden");
    }

    public int GetCurrentLevel()
    {
        return level;
    }
}
