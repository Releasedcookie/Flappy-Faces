using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelStart : MonoBehaviour
{
    public string sceneName;
    public int highestScore = 0;
    public TextMeshProUGUI highestScoreText;

    public void Awake()
    {
        if (!PlayerPrefs.HasKey("KeyName"))
        {
            //Debug.Log("No Score Value");
            PlayerPrefs.SetInt("KeyName", 0);
        }
        highestScore = PlayerPrefs.GetInt("KeyName");
        highestScoreText.text = ("High Score: " + highestScore);

        PlayerPrefs.SetInt("DeathNumber", 0);
    }
    
    public void startGame()
    {
        SceneManager.LoadScene(sceneName);
    }
}
