using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LeaderboardScript : MonoBehaviour
{
    public Transform table;
    public GameObject row;

    public void UpdateLeaderboard(){
        
        GameManager.manager.leaders.Sort((p1,p2)=>p2.score.CompareTo(p1.score));
        for (int i = 0; i < GameManager.manager.leaders.Count; i++)
        {
            PersonalScore personalScore = GameManager.manager.leaders[i];
            GameObject playerRow = Instantiate(row, table);
            TextMeshProUGUI[] texts = playerRow.GetComponentsInChildren<TextMeshProUGUI>();
            texts[0].text = (i+1).ToString();
            texts[1].text = personalScore.name;
            texts[2].text = personalScore.score.ToString();
        }
    }
}
