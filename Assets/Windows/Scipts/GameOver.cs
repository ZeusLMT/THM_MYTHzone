using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour{

    public Text ScoreValue;
    private float Delay = 0;
    private float ScoreDelay = 0.7f;
    private bool Showed = false;
    private bool loadLock;

    public void ClearScore()
    {
        ScoreValue.text = "";
    }

    public void Start()
    {
        StartWindow.canContinue = true;
        Delay = 0;
        ClearScore();
        Showed = false;
    }


    public void ShowScore(Text Value)
    {
        Value.text = Player.Score.ToString("D6");
        Showed = true;
    }

    public void MainMenu()
    {
        if (!loadLock) loadScene("MainMenu");
    }
    public void Restart()
    {
        if(!loadLock) loadScene(PlayerPrefs.GetString("currentscene"));
    }
    void loadScene(string Scene)
    {
        loadLock = true;
        Player.Score = 0;
        SceneManager.LoadScene(Scene);
    }

    void Update()
    {
        Delay += Time.deltaTime;
        if (Delay > ScoreDelay && !Showed) ShowScore(ScoreValue);
    }
}
