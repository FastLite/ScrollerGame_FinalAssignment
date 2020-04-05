using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowHighScore : MonoBehaviour
{
    public TextMeshProUGUI highScoreTextField;


    public void ShowHIghScore()
    {
        highScoreTextField.text = PlayerPrefs.GetInt("highScore").ToString();
    }
}
