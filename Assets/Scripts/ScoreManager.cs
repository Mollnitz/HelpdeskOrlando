using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using System;

public class ScoreManager : MonoBehaviour
{
    public Dictionary<string, int> highscores;

    private void LoadHighScores()
    {
        //Make sure we have a fresh state
        highscores = new Dictionary<string, int>();

        if (PlayerPrefs.HasKey("Highscores") && PlayerPrefs.GetString("Highscores") != "")
        {

            string high = PlayerPrefs.GetString("Highscores");
            var ser = new DataContractJsonSerializer(typeof(List<HighScore>));
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(high));
            List<HighScore> data = ser.ReadObject(stream) as List<HighScore>;

            foreach (var item in data)
            {
                highscores.Add(item.name, item.score);
            }
        }
        else
        {
            PlayerPrefs.SetString("Highscores", "");
        }
    }

    private void AddHighScore(string name, int value)
    {
        //If you beat the score.
        if (highscores.ContainsKey(name) && highscores[name] < value)
        {
            Debug.Log("New highscore!");
            highscores[name] = value;
        }
        //First score for the name.
        else if (!highscores.ContainsKey(name))
        {
            highscores.Add(name, value);
        }
    }
    private void SaveHighScores()
    {
        var ser = new DataContractJsonSerializer(typeof(List<HighScore>));
        var memory = new MemoryStream();
        List<HighScore> dataToSave = new List<HighScore>();

        foreach (var item in highscores)
        {
            dataToSave.Add(new HighScore(item.Key, item.Value));
        }
        ser.WriteObject(memory, dataToSave);
        memory.Flush();

        memory.Position = 0;
        StreamReader sr = new StreamReader(memory);

        string save;
        save = sr.ReadToEnd();

        PlayerPrefs.SetString("Highscores", save);
        PlayerPrefs.Save();
    }

    float points = 0;

    private void Awake()
    {
        LoadHighScores();
    }

    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.instance != null)
        {
            GameManager.PointEvent.AddListener(x => points += x);
            GameManager.levelClear.AddListener(() => {
                StartCoroutine(DelayHighscore());
                
                
                }
            );
        }
        

    }

    private IEnumerator DelayHighscore()
    {
        yield return new WaitForSeconds(0.2f);
        AddHighScore(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name, (int)points);
        SaveHighScores();
        Debug.Log("Saved");
    }

    // Update is called once per frame
    void Update()
    {
        /*  //Don't touch - sssh
        if (Input.GetKeyDown(KeyCode.K))
        {
            AddHighScore(Random.Range(-999, 999).ToString(), Random.Range(-999, 999));
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Loading");
            LoadHighScores();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("Saving");
            SaveHighScores();
        } 
        if (Input.GetKeyDown(KeyCode.P))
        {
            PlayerPrefs.SetString("Highscores", "");
        }*/

    }
}
