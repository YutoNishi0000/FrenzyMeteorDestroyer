using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 180.0f; // 制限時間（秒）
    public TextMeshProUGUI timeText; // UIに表示するためのテキスト

    private bool isTimeUp = false; // 制限時間が終了したかどうかのフラグ

    void Update()
    {
        if (timeRemaining > 0 && !isTimeUp)
        {
            timeRemaining -= Time.deltaTime; // 時間を減算する
            DisplayTime(timeRemaining); // UIに時間を表示する
        }
        else if(timeRemaining <= 0 && !isTimeUp)
        {
            isTimeUp = true; // 制限時間が終了したことをフラグで管理する
            timeRemaining = 0; // 制限時間を0にする
            timeText.text = "Time up"; // UIに「Time up」と表示する
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
