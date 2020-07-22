using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameRules : MonoBehaviour
{
    public static bool gameIsRunning = true;
    public static int currentLevel = 0;
    public static GameRules current;

    private AudioSource songSource;
    public static int currentSong = 0;
    public static int currentSongBuffer = 0;
    public static float currentSongTime = 0f;

    public AudioClip autumnSong;
    public AudioClip desertSong;
    public AudioClip windSong;

    //private float[] autumnSongPositions =
    //{
    //    0f, 29.561f, 59.103f, 88.640f, 118.199f, 132.937f, 147.721f, 177.273f, 206.814f, 221.614f, 236.328f, 251.108f, 280.596f, 310.595f
    //};
    


    public bool levelFinished = false;

    public static StageInfo[] stages = {
        new StageInfo("First Steps", "1-1", 4f, 0, true),
        new StageInfo("Rabbit Holes", "1-2", 6f, 0),
        new StageInfo("Skipping Stones", "1-3", 8f, 0),
        new StageInfo("Soar Through The Skies!!!", "1-4", 12f, 0),
        new StageInfo("Turtle Stew", "1-5", 5f, 0),
        new StageInfo("Onto New Adventures", "1-6", 5f, 0),
        new StageInfo("Deserts", "2-1", 4.5f, 1),
        new StageInfo("The Squeeze", "2-2", 6f, 1),
        new StageInfo("Dangerous Descension", "2-3", 9.5f, 1),
        new StageInfo("In, Over 'n Out", "2-4", 17.5f, 1),
        new StageInfo("Into the Chamber", "2-5", 5.5f, 1),
        new StageInfo("Grand Escape", "2-6", 19f, 1),
        new StageInfo("The Climb", "3-1", 20f, 2),
        new StageInfo("Glacial Caves", "3-2", 20f, 2),
        new StageInfo("One Last Stretch", "3-3", 15f, 2),
    };

    public float timer = 0f;
    public float finalTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        current = this;
        GameMenu.current.nextButton.onClick.AddListener(NextLevel);
        gameIsRunning = true;
        PlaySong();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameRules.gameIsRunning)
        {
            timer += Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.R)) ResetLevel();

        if (Input.GetKeyDown(KeyCode.Escape)) TogglePause();
    }

    public void EndLevel()
    {
        
        gameIsRunning = false;
        finalTime = timer;

        if (GameRules.stages[currentLevel].playerBestTime == 0f || GameRules.stages[currentLevel].playerBestTime > finalTime)
        {
            GameRules.stages[currentLevel].playerBestTime = finalTime;
        }

        UnlockLevel(currentLevel + 1);
        GameMenu.current.ShowNextLevelButton();
        GameMenu.current.SetFinalTime(finalTime);
        GameMenu.current.SetBeatDeveloperTime(finalTime, stages[currentLevel].developerTime);

        PauseGame();
        levelFinished = true;
    }

    public void TogglePause()
    {
        if (levelFinished) return;

        if (gameIsRunning)
            PauseGame();
        else
            UnpauseGame();
    }

    public void PauseGame()
    {
        gameIsRunning = false;
        DisplayUI();
    }

    public void UnpauseGame()
    {
        gameIsRunning = true;
        UndisplayUI();
    }

    public void DisplayUI()
    {
        GameMenu.menu.SetActive(true);
    }

    public void UndisplayUI()
    {
        GameMenu.menu.SetActive(false);
    }

    public void NextLevel()
    {
        string levelName = GameRules.stages[currentLevel + 1].stagePath;
        currentLevel = currentLevel + 1;
        

        currentSongTime = songSource.time;
        SceneManager.LoadScene(levelName, LoadSceneMode.Single);
        StartLevel();
    }

    public void StartLevel()
    {
        gameIsRunning = true;
        timer = 0f;
    }

    public static void StartLevel(string levelId)
    {
        SceneManager.LoadScene(levelId, LoadSceneMode.Single);
        gameIsRunning = true;
    }

    public void ResetLevel()
    {
        currentSongTime = songSource.time;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        StartLevel();
    }

    // player death
    public void StopGame()
    {
        gameIsRunning = false;
        GameMenu.current.showRestartText();

    }

    void PlaySong()
    {
        if (stages[currentLevel].song != currentSong)
        {
            currentSong = stages[currentLevel].song;
            currentSongTime = 0f;
        } 

        if (currentSong == 0)
        {
           if (autumnSong.length < currentSongTime)
            {
                currentSongTime = 0f;
            }

            songSource = AudioTool.ShootAudio(autumnSong, transform, 0.5f, currentSongTime);
        }

        if (currentSong == 1)
        {
            if (desertSong.length < currentSongTime)
            {
                currentSongTime = 0f;
            }

            songSource = AudioTool.ShootAudio(desertSong, transform, 0.5f, currentSongTime);
        }

        if (currentSong == 2)
        {
            if (windSong.length < currentSongTime)
            {
                currentSongTime = 0f;
            }

            songSource = AudioTool.ShootAudio(windSong, transform, 0.5f, currentSongTime);
        }
    }

    public void UnlockLevel(int id)
    {
        if (id >= GameRules.stages.Length) return;
        GameRules.stages[id].stageUnlocked = true;
    }
}
