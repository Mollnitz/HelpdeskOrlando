using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class MenuShowHighscores : MonoBehaviour
{
    Text textF;
    // Start is called before the first frame update
    void Start()
    {
        textF = GetComponent<Text>();
        var highscoreRef = GetComponent<ScoreManager>().highscores;
        string highScoreText = "HIGH SCORES!" + Environment.NewLine;
        highscoreRef.ToList().ForEach(x => highScoreText += " " + x.Key + " " + x.Value.ToString() + Environment.NewLine);

        textF.text = highScoreText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
