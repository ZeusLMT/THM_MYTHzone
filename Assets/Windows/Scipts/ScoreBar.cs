using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBar : MonoBehaviour {
    public Text Score;

	void Start () {
        Score = GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Player.Instance != null) Score.text = Player.Score.ToString();
        else return;
	}
}
