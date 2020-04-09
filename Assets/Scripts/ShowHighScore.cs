using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowHighScore : MonoBehaviour
{
    public TextMeshProUGUI highScoreTextField;

    private void Start()
    {
        Debug.Log("current highscore is " + PlayerPrefs.GetInt("highScore"));
        highScoreTextField.text = "current player highscore is: " + PlayerPrefs.GetInt("highScore").ToString();
    }
    
}
