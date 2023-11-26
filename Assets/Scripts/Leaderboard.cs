using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    private string leaderboardKey = "15480";
    public TextMeshProUGUI playerNames;
    public TextMeshProUGUI playerScores;

    public IEnumerator SubmitScoreRoutine(int score)
    {
        bool done = false;
        string memberId = PlayerPrefs.GetString("PlayerID");
        // Debug.Log("========= Saving High Score " + memberId + score);
        LootLockerSDKManager.SubmitScore(memberId, score, leaderboardKey, (response) =>
        {
            if (response.statusCode == 200)
            {
                Debug.Log("Successful Score Update");
                done = true;
            }
            else
            {
                Debug.Log("Failed to update online leaderboard");
                done = true;
            }
        });
    yield return new WaitWhile(() => done == false);

    }

    public IEnumerator FetchTopHighscoresRoutine()
    {
        bool done = false;
        LootLockerSDKManager.GetScoreList(leaderboardKey, 10, 0, (response) =>
        {
            if (response.statusCode == 200)
            {
                string tempPlayerNames = "Player\n";
                string tempPlayerScores = "Score\n";

                LootLockerLeaderboardMember[] members = response.items;

                for (int i = 0; i < members.Length; i++)
                {
                    tempPlayerNames += members[i].rank + ". " + members[i].member_id + "\n";
                    tempPlayerScores += members[i].score + "\n";


                    /*tempPlayerNames += members[i].rank + ". ";
                    if (members[i].player.name != "")
                    {
                        tempPlayerNames += members[i].player.name;
                    }
                    else
                    {
                        tempPlayerNames += members[i].player.id;
                    }
                    tempPlayerScores += members[i].score + "\n";
                    tempPlayerNames += "\n";*/
                }
                done = true;
                playerNames.text = tempPlayerNames;
                playerScores.text = tempPlayerScores;
            }
            else
            {
                Debug.Log("Failed" + response.Error);
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);

    }
}
