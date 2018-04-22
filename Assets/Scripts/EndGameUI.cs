using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System;

public class EndGameUI : MonoBehaviour {

    private Text Ranks;

    void Start()
    {
        var texts = GetComponentsInChildren<Text>();
        Ranks = texts.FirstOrDefault(x => x.name == "Ranks");
        string ranksString = "";
        foreach(var score in GameManager.Instance.GetAllScores())
        {
            ranksString += score.GetRank() + Environment.NewLine;
        }
        Ranks.text = ranksString;
    }
}
