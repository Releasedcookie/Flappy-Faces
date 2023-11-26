using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LogicScript : MonoBehaviour
{
    public int playerScore = 0;
    public int highestScore = 0;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI highestScoreText;
    public GameObject gameOverScreen;

    public float timer = 0;
    public int seconds = 0;
    public int setLevel = 1;
    public int MoveSpeed = 5;
    public float SpawnRate = 2;
    public int gameCounter = 0;

    public Leaderboard leaderboard;


    // On Game Start
    public void Awake()
    {
        if (!PlayerPrefs.HasKey("KeyName"))
        {
            PlayerPrefs.SetInt("KeyName", 0);
        }
        highestScore = PlayerPrefs.GetInt("KeyName");
        highestScoreText.text = ("High Score: " + highestScore);

        level(1);
    }

    public void Update()
    {
        timer = timer + Time.deltaTime;
        seconds = Mathf.RoundToInt(timer);

        int autoLevel = (Mathf.RoundToInt(seconds / 15) + 1);

        if (autoLevel != setLevel)
        {
            // Debug.Log("Level " + autoLevel);
            level(autoLevel);
        }
        setLevel = autoLevel;

    }

    public void addScore(int scoreToAdd)
    {
        if (gameOverScreen.activeInHierarchy == false)
        {
            playerScore = playerScore + scoreToAdd;
            ScoreText.text = playerScore.ToString();
        }
    }

    [ContextMenu("Restart Game")]
    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void gameOver()
    {

        if (!PlayerPrefs.HasKey("DeathNumber"))
        {
            gameCounter = 0;
            PlayerPrefs.SetInt("DeathNumber", 0);
        }
        gameCounter = PlayerPrefs.GetInt("DeathNumber");
        gameCounter++;
        PlayerPrefs.SetInt("DeathNumber", gameCounter);

        Debug.Log("Death Number: " + gameCounter);
        if (gameCounter == 4)
        {
            gameCounter = 0;
            PlayerPrefs.SetInt("DeathNumber", gameCounter);
            GoogleAds.instance.ShowAd();
            //FindObjectOfType<GoogleAds>().ShowAd();
        }


        StartCoroutine(DieRoutine());


    }

    IEnumerator DieRoutine()
    {
        gameOverScreen.SetActive(true);
        yield return leaderboard.FetchTopHighscoresRoutine();

        if (playerScore > highestScore)
        {
            Debug.Log("New High Score");
            PlayerPrefs.SetInt("KeyName", playerScore);
            PlayerPrefs.Save();
            yield return leaderboard.SubmitScoreRoutine(playerScore);
            highestScoreText.text = ("High Score: " + PlayerPrefs.GetInt("KeyName"));
        }

    }

    [ContextMenu("Reset Score to 0")]
    public void resetScoreTo0()
    {
        PlayerPrefs.SetInt("KeyName", 0);
        PlayerPrefs.Save();
    }

    public void level(int level)
    {
        switch (level)
        {
            case 1:
                MoveSpeed = 5;
                SpawnRate = 3.5f;
                break;
            case 2:
                MoveSpeed = 7;
                SpawnRate = 3f;
                break;
            case 3:
                MoveSpeed = 10;
                SpawnRate = 2.5f;
                break;
            case 4:
                MoveSpeed = 12;
                SpawnRate = 2f;
                break;
            case 5:
                MoveSpeed = 14;
                SpawnRate = 1.5f;
                break;
            case 6:
                MoveSpeed = 16;
                SpawnRate = 1f;
                break;
        }
    }

}
