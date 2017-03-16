using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WelcomeText : MonoBehaviour {
    public Text welcomeText;
	// Use this for initialization
	void Start () {
        welcomeText.text = "Welcome to MYTHzone, " + KeyboardWindow.playerName + "!";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
