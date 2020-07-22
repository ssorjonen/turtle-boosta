using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectButton : MonoBehaviour
{
    public string levelId;
    public int levelInt;
    public Text text;


    public void LoadLevel()
    {
        GameRules.currentLevel = levelInt;
        SceneManager.LoadScene(levelId, LoadSceneMode.Single);
    }
}
