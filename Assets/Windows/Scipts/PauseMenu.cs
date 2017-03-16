using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    static public bool Paused;
    [SerializeField]
    private GameObject PausePanel;
    [SerializeField]
    private GameObject firstSelected;
    private EventSystem eventSystem
    {
        get { return GameObject.Find("EventSystem").GetComponent<EventSystem>(); }
    }
    private bool loadLock;
    private Text soundText;
    [SerializeField]
    private bool soundIsOn;
    // Use this for initialization
    void Start () {
        soundText = GameObject.Find("Sound").GetComponentInChildren<Text>();
        loadLock = false;
        PausePanel = GameObject.Find("PauseMenu");
        Paused = false;
        firstSelected = GameObject.Find("Resume");
        if (AudioListener.volume != 0) soundIsOn = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Paused = !Paused;
        }

        if (Paused == true)
        {
            PausePanel.SetActive(true);
            if (eventSystem.currentSelectedGameObject == null) OnFocus(firstSelected);
            Time.timeScale = 0;
        }
        if (Paused == false)
        {
            PausePanel.SetActive(false);
            Time.timeScale = 1;
            OnFocus(null);
        }

    }
    public void OnFocus(GameObject Button)
    {
        eventSystem.SetSelectedGameObject(Button);
    }

    public void Resume()
    {
        Paused = false;
    }

    public void Restart()
    {
        Player.Score = 0;
        if(!loadLock) loadScene(SceneManager.GetActiveScene().name);
    }

    public void backToMenu()
    {
        if (!loadLock)
        {
            PlayerPrefs.SetString("currentscene", SceneManager.GetActiveScene().name);
            StartWindow.canContinue = true;
            loadScene("MainMenu");
        }
    }

    public void sound()
    {
        if (soundIsOn)
        {
            soundIsOn = false;
            soundText.text = "Sound: Off";
            AudioListener.volume = 0;
        }
        else
        {
            soundIsOn = true;
            soundText.text = "Sound: On";
            AudioListener.volume = 1;
        }
    }

    public void loadScene(string scene)
    {
        loadLock = true;
        SceneManager.LoadScene(scene);
    }
}
