using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Tutorial")]
    [SerializeField] Sprite[] tutorialSprites;
    int currentTutorialIndex = 0;
    [SerializeField] GameObject tutorialPanel;
    [SerializeField] Image tutorialImage;
    [SerializeField] Button nextTutorial;
    [SerializeField] Button previousTutorial;


    #region Buttons
    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }

    #region Tutorial
    public void CloseTutorial()
    {
        tutorialPanel.SetActive(false);
    }

    public void NextTutorialButton()
    {
        currentTutorialIndex++;
        if (currentTutorialIndex == tutorialSprites.Length - 1)
            nextTutorial.interactable = false;

        previousTutorial.interactable = true;

        tutorialImage.sprite = tutorialSprites[currentTutorialIndex];
    }

    public void PreviousTutorialButton()
    {
        currentTutorialIndex--;
        if (currentTutorialIndex == 0)
            previousTutorial.interactable = false;

        nextTutorial.interactable = true;

        tutorialImage.sprite = tutorialSprites[currentTutorialIndex];
    }
    #endregion Tutorial
    public void HowToPlayButton()
    {
        tutorialPanel.SetActive(true);

        currentTutorialIndex = 0;
        previousTutorial.interactable = false;
        nextTutorial.interactable = true;
    }

    public void ExitButton()
    {
        Application.Quit();
    }
    #endregion Buttons
}
