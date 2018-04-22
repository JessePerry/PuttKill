using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelScore : MonoBehaviour
{
    public int Shots;
    public int SecondsTaken;
    public int ParShots;
    public int ParSecondsTaken;

    private string[] ranksAndQuotes;
    private DateTime start;

    private void Start()
    {
        start = DateTime.UtcNow;
    }

    public void FixedUpdate()
    {
        var seconds = (DateTime.UtcNow - start).TotalSeconds;
        var smallSeconds = seconds > 9999 ? 9999 : (int)seconds;
        SecondsTaken = smallSeconds;
    }

    public string GetRank()
    {
        if (Shots < ParShots && SecondsTaken < ParSecondsTaken)
            return "A+";
        else if (Shots < ParShots)
            return "B";
        else if (SecondsTaken < ParSecondsTaken)
            return "C";
        else
            return "D";
    }

    public string GetQuote()
    {
        if (ranksAndQuotes == null || ranksAndQuotes.Length == 0)
            SetupQuotes();
        var index = 0;
        while (index == GameManager.Instance.lastQuoteIndex)
        {
            index = UnityEngine.Random.Range(0, ranksAndQuotes.Length);
        }
        GameManager.Instance.lastQuoteIndex = index;
        return ranksAndQuotes[index];
    }

    private void SetupQuotes()
    {
        ranksAndQuotes = new string[]
        {
            "He who controls the putting, controls history.",
            "I'm just a ball who's good at doing what they do. Killing."  ,
            "It's easy to forget what a sin is in the middle of the putt." ,
            "Why do we putt, just to suffer?"
        };
    }
}
