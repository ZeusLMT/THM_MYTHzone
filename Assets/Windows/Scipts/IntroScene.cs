using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScene : MonoBehaviour {
    private bool loadLock;
    [SerializeField]
    private float Delay = 4.5f;

	// Use this for initialization
	void Start () {
        loadLock = false;
	}
	
	// Update is called once per frame
	void Update () {
        Delay -= Time.deltaTime;
        if(Delay <= 0)
        {
            if(!loadLock) LoadScene();
        }
	}
    public void LoadScene()
    {
        loadLock = true;
        SceneManager.LoadScene("Story");
    }
}
