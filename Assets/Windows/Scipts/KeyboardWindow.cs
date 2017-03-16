using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardWindow : GenericWindow {
    public Text inputField;
    public int maxCharacters = 10;
    static public string playerName;
    private float delay = 0;
    private float cursorDelay = .5f;
    private bool blink = false;
    private string inputText = "";
    public AudioClip Click;
    private AudioSource AudioPlayer;

    void Start()
    {
        AudioPlayer = gameObject.AddComponent<AudioSource>();
    }
    public void Update()
    {
        var text = inputText;
        if (text.Length < maxCharacters)
        {
            text += "_";
            if (blink)
            {
                text = text.Remove(text.Length - 1);
            }
        }
        inputField.text = text;
        delay += Time.deltaTime;
        if (delay > cursorDelay)
        {
            delay = 0;
            blink = !blink;
        }
    }

    public void onKeyPress(string key)
    {
        if (Click)
        {
            AudioPlayer.PlayOneShot(Click, 0.5f);
        }
        if (inputText.Length < maxCharacters)
        {
            inputText += key;
        }
    }

    public void clearAll()
    {
        if (Click)
        {
            AudioPlayer.PlayOneShot(Click, 0.5f);
        }
        inputText = "";
    }

    public void Del()
    {
        if (Click)
        {
            AudioPlayer.PlayOneShot(Click, 0.5f);
        }
        inputText = inputText.Remove(inputText.Length - 1);
    }

    public void Enter()
    {
        playerName = inputText;
        Manager.Open(1);
    }

    public override void Open()
    {
        base.Open();
    }

    public override void Close()
    {
        base.Close();
    }
}
