using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public GameObject selector;

    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
        foreach(StageInfo stage in GameRules.stages)
        {
            Vector2 position = selector.transform.position;
            position.x += (selector.GetComponent<RectTransform>().rect.width * (i % 3));
            position.y -= (selector.GetComponent<RectTransform>().rect.height * Mathf.Ceil(i / 3));
            position.y -= 15 * Mathf.Ceil(i / 6);

            GameObject levelSelector = Instantiate(selector, position, selector.transform.rotation, this.transform);

            

            LevelSelectButton buttonScript = levelSelector.GetComponent<LevelSelectButton>();
            buttonScript.levelId = stage.stagePath;
            buttonScript.levelInt = i;
            levelSelector.GetComponent<Button>().onClick.AddListener(buttonScript.LoadLevel);

            if (!stage.stageUnlocked)
            {
                buttonScript.text.text = "Stage not unlocked";
            }

            else
            {
               buttonScript.text.text = stage.stageName + "\n" + stage.playerBestTime;
            }

            i++;
        }
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
