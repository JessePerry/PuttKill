using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LevelEndUI : MonoBehaviour
{
    private Text ParColumnText;
    private Text ActualColumnText;
    private Text LabelsColumnText;
    private Text QuoteText;
    private Image QuoteImage;
    private Image Panel;

    private int stepsCount = 0;
    private LevelScore currentLevelScore;
    private DateTime start;

    public void Reset()
    {
        var texts = GetComponentsInChildren<Text>();
        ParColumnText = texts.FirstOrDefault(x => x.name == "ParColumnText");
        ActualColumnText = texts.FirstOrDefault(x => x.name == "ActualColumnText");
        LabelsColumnText = texts.FirstOrDefault(x => x.name == "LabelsColumnText");
        QuoteText = texts.FirstOrDefault(x => x.name == "QuoteText");
        LabelsColumnText.text = "";
        ActualColumnText.text = "";
        ParColumnText.text = "";
        QuoteText.text = "";
        var images = GetComponentsInChildren<Image>();
        QuoteImage = images.FirstOrDefault(x => x.name == "QuoteImage");
        QuoteImage.enabled = false;
        Panel = images.FirstOrDefault(x => x.name == "Panel");
        Panel.enabled = false;
        stepsCount = 0;
    }

    void Start()
    {
        Reset();
        start = DateTime.UtcNow;
    }

    private void Update()
    {
        if (DateTime.UtcNow - start > TimeSpan.FromSeconds(1))
        {
            ShowNextUI();
            start = DateTime.UtcNow;
        }
    }

    private void ShowNextUI()
    {
        stepsCount++;
        currentLevelScore = GameManager.Instance.GetCurrentLevelScore();
        if (stepsCount == 1)
        {
            LabelsColumnText.text = Environment.NewLine + "Shots";
            ParColumnText.text = "Par" + Environment.NewLine + currentLevelScore.ParShots;
        }
        else if (stepsCount == 2)
        {
            ActualColumnText.text = "Actual" + Environment.NewLine + currentLevelScore.Shots;
        }
        else if (stepsCount == 3)
        {
            LabelsColumnText.text = LabelsColumnText.text + Environment.NewLine + "Time";
            ParColumnText.text = ParColumnText.text + Environment.NewLine + SecondsToString(currentLevelScore.ParSecondsTaken);
        }
        else if (stepsCount == 4)
        {
            ActualColumnText.text = ActualColumnText.text + Environment.NewLine + SecondsToString(currentLevelScore.SecondsTaken);
        }
        else if (stepsCount == 5)
        {
            LabelsColumnText.text = LabelsColumnText.text + Environment.NewLine + "Rank";
        }
        else if (stepsCount == 6)
        {
            ActualColumnText.text = ActualColumnText.text + Environment.NewLine + currentLevelScore.GetRank();
        }
        else if (stepsCount == 7)
        {
            QuoteText.text = currentLevelScore.GetQuote();
            QuoteImage.enabled = true;
            Panel.enabled = true;
        }
        else if (stepsCount == 10)
        {
            GameManager.Instance.GoToNextLevel();
        }
    }

    private string SecondsToString(int seconds)
    {
        TimeSpan ts = new TimeSpan(0, 0, seconds);
        return ts.Minutes.ToString() + ":" + ts.Seconds;
    }
}
