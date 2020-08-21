using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    private Transform entryContainor;
    private Transform entryTemplate;
    private int[] scores;
    private List<HighScoreEntry> highScoreEntryList;
    private List<Transform> highscoreentryTransformList;
    private void Awake() {
        entryContainor = transform.Find("ScoreBoard Containor");
        entryTemplate = entryContainor.Find("HighscoreEntryTemplate");
        entryTemplate.gameObject.SetActive(false);
        //AddHighScoreEntry(1000,"ESA");
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        HighScores highscores = JsonUtility.FromJson<HighScores>(jsonString);
        if(highscores.highscoreEntryList == null){
            highScoreEntryList = new List<HighScoreEntry>();
        }else{
            highScoreEntryList = highscores.highscoreEntryList;
        }
        

        for(int i = 0; i < highScoreEntryList.Count;i++){
            for(int j = i+1;j<highScoreEntryList.Count;j++){
                if(highScoreEntryList[j].score > highScoreEntryList[i].score){
                    HighScoreEntry temp = highScoreEntryList[i];
                    highScoreEntryList[i] = highScoreEntryList[j];
                    highScoreEntryList[j] = temp;
                }
            }
        }
        

        highscoreentryTransformList = new List<Transform>();
        foreach(HighScoreEntry entry in highScoreEntryList){
            CreateHighScoreEntry(entry,entryContainor,highscoreentryTransformList);
        }
        
        
        
    }

    public void AddHighScoreEntry(int score, string name){
        HighScoreEntry highscoreentry = new HighScoreEntry{score = score, name = name};
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        HighScores highscores = JsonUtility.FromJson<HighScores>(jsonString);
        if(highscores == null){
            highscores = new HighScores{highscoreEntryList = new List<HighScoreEntry>()};
        }
        highscores.highscoreEntryList.Add(highscoreentry);
        string json =JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable",json);
        PlayerPrefs.Save();
        
    }

    private void CreateHighScoreEntry(HighScoreEntry highScoreEntry, Transform container, List<Transform> transformList){
        float templateHeight = 40f;
        Transform entryTransform = Instantiate(entryTemplate,container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0,-templateHeight*transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count +1;
        string rankString;
        switch(rank){
            default:
                rankString = rank + "TH";break;
            case 1: rankString = "1ST";break;
            case 2: rankString = "2ND";break;
            case 3: rankString = "3RD";break;
        }
        entryTransform.Find("Pos").GetComponent<Text>().text = rankString;

        int score = highScoreEntry.score;
        entryTransform.Find("score").GetComponent<Text>().text = score +"";
        string name = highScoreEntry.name;
        entryTransform.Find("name").GetComponent<Text>().text = name;
        entryTransform.Find("BackGround").gameObject.SetActive(rank%2==0);
        transformList.Add(entryTransform);
    }

    private class HighScores{
        public List<HighScoreEntry> highscoreEntryList;
    }

    [System.Serializable]
    public class HighScoreEntry{
        public int score;
        public string name;

    }
}
