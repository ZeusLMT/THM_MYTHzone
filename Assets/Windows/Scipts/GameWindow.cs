using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWindow : GenericWindow {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Player.Instance.IsDead || Player.Instance == null)
        {
            Manager.Open(3);
        }
	}
}
