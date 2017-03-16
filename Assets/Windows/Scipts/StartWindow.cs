using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartWindow : GenericWindow {
    public Text welcomeText;
    static public bool canContinue;
	public Button continueButton;
    private bool loadLock;
    public Toggle Sound;
    public AudioClip Click;
    public AudioClip EnterKey;
    private AudioSource AudioPlayer;

    public void Start()
    {
        loadLock = false;
        AudioPlayer = gameObject.AddComponent<AudioSource>();
        if(EnterKey) AudioPlayer.PlayOneShot(EnterKey);
        Player.Score = 0;
    }
	public override void Open ()
    {
        WindowManager.defaultWindowID = 1;
        loadLock = false;

		continueButton.gameObject.SetActive (canContinue);

		if (continueButton.gameObject.activeSelf) {
			firstSelected = continueButton.gameObject;
		}

        welcomeText.text = "Welcome to MYTHzone, " + KeyboardWindow.playerName + "!";

        base.Open ();
	}

	public void NewGame(){
        if (Click)
        {
            AudioPlayer.PlayOneShot(Click);
        }
        if (!loadLock)
        {
            loadScene("Level1");
        }
	}

	public void Continue(){
        if (Click)
        {
            AudioPlayer.PlayOneShot(Click);
            if(!loadLock){
                loadScene(PlayerPrefs.GetString("currentscene"));
            }
        }
    }

	public void Exit(){
        if (Click)
        {
            AudioPlayer.PlayOneShot(Click, 0.5f);
        }
        Application.Quit();
    }
    public void SoundToggle()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (Click)
            {
                AudioPlayer.PlayOneShot(Click, 0.5f);
            }
            if (Sound.isOn) Sound.isOn = false;
            else Sound.isOn = true;
        }
    }

    public void SoundOnOff()
    {
        if (Sound.isOn) AudioListener.volume = 1;
        else if (!Sound.isOn) AudioListener.volume = 0;
    }

    
    void Update()
    {
        SoundToggle();
        SoundOnOff();
    }

    void loadScene(string nextScene)
    {
        loadLock = true;
        SceneManager.LoadScene(nextScene);
    }
	
}
