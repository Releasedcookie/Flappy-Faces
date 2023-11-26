using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StartGame : MonoBehaviour
{
    public string sceneName;
    public int highestScore = 0;
    TouchScreenKeyboard keyboard;

    // Get Game Objects
    public TextMeshProUGUI highestScoreText;
    public TextMeshProUGUI userName;
    public GameObject startButton;
    public PlayerManager playerManager;

    public void Awake()
    {
        if (!PlayerPrefs.HasKey("KeyName"))
        {
            Debug.Log("No Score Value");
            PlayerPrefs.SetInt("KeyName", 0);
        }
        highestScore = PlayerPrefs.GetInt("KeyName");
        highestScoreText.text = ("High Score: " + highestScore);

    }

    public void Update()
    {
        var text = userName.text.ToUpper();

        if(userName.text.Length <= 1)
        {
            startButton.SetActive(false);
        }
        else
        {
            startButton.SetActive(true);
        }
    }

    public void startGame()
    {
        string Username = userName.text.ToUpper();
        PlayerPrefs.SetString("PlayerID", Username);
        PlayerPrefs.Save();
        //Debug.Log("Setting Username: " + Username);
        //playerManager.LoginRoutine();
        SceneManager.LoadScene(sceneName);

    }

    public void OpenKeyboard()
    {
        keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
    }
}