using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public GameObject menuPrefab;
    public static GameObject menu;
    public static GameMenu current;
    public Text finalTime;
    public Text developerTime;

    public Button nextButton;
    public GameObject nextButtonObject;
    public Button restartButton;
    public Button menuButton;

    public GameObject restartInfo;
    


    // Start is called before the first frame update
    void Awake()
    {
        menu = menuPrefab;
        current = this;
    }

    public void SetFinalTime(float time)
    {
        finalTime.text = "Final time: " + time + " seconds";
    }

    public void SetBeatDeveloperTime(float time, float fDeveloperTime)
    {
        if (time < fDeveloperTime)
        {
            developerTime.text = "You beat challenge time of " + fDeveloperTime + "!";
        }
    }

    public void showRestartText()
    {
        restartInfo.SetActive(true);
    }

    public void RestartLevel()
    {
        GameRules.current.ResetLevel();
    }

    public void BackToMainMenu()
    {
        GameRules.currentSongTime = 0f;
        GameRules.gameIsRunning = true;
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void ShowNextLevelButton()
    {
        if (GameRules.currentLevel >= GameRules.stages.Length - 1) return;
        nextButtonObject.SetActive(true);
    }
}
