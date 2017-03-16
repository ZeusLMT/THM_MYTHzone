using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    [SerializeField]
    private GameObject overPanel;
    public Text ScoreValue;
    private float Delay = 0;
    private float ScoreDelay = 2;
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
        if(!loadLock) loadScene("MainMenu");
    }
    void loadScene(string Scene)
    {
        loadLock = true;
        SceneManager.LoadScene(Scene);
    }

    void Update()
    {
        Delay += Time.deltaTime;
        if (Delay >= ScoreDelay && !Showed)
        {
            ShowScore(ScoreValue);
            overPanel.SetActive(true);
        }
    }
}
