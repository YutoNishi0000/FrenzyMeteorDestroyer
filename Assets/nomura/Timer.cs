using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 180.0f; // �������ԁi�b�j
    public TextMeshProUGUI timeText; // UI�ɕ\�����邽�߂̃e�L�X�g

    private bool isTimeUp = false; // �������Ԃ��I���������ǂ����̃t���O

    void Update()
    {
        if (timeRemaining > 0 && !isTimeUp)
        {
            timeRemaining -= Time.deltaTime; // ���Ԃ����Z����
            DisplayTime(timeRemaining); // UI�Ɏ��Ԃ�\������
        }
        else if(timeRemaining <= 0 && !isTimeUp)
        {
            isTimeUp = true; // �������Ԃ��I���������Ƃ��t���O�ŊǗ�����
            timeRemaining = 0; // �������Ԃ�0�ɂ���
            timeText.text = "Time up"; // UI�ɁuTime up�v�ƕ\������
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
