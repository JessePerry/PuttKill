using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LevelPlayingUI : MonoBehaviour {
    private Text ParValue;
    private Text ShotsValue;
    private LevelScore currentLevelScore;

    public void Reset()
    {
        var texts = GetComponentsInChildren<Text>();
        ParValue = texts.FirstOrDefault(x => x.name == "ParValue");
        ShotsValue = texts.FirstOrDefault(x => x.name == "ShotsValue");
        ParValue.text = GameManager.Instance.GetCurrentLevelScore().ParShots.ToString();
    }
    // Use this for initialization
    void Start () {
        Reset();
	}

    private void Update()
    {
        ShotsValue.text = GameManager.Instance.GetCurrentLevelScore().Shots.ToString();
    }

}
