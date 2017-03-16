using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBarRunning : MonoBehaviour {
    public Text Score;
    void Start()
    {
        Score = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Score.text = Player.Score.ToString();
        return;
    }
}
