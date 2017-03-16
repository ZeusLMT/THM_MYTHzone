using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Story : MonoBehaviour {
    private bool loadLock;
    [SerializeField]
    private float Delay = 22;

    // Use this for initialization
    void Start()
    {
        loadLock = false;
    }

    // Update is called once per frame
    void Update()
    {
        Delay -= Time.deltaTime;
        if (Delay <= 0 || Input.GetKeyDown(KeyCode.Space))
        {
            if (!loadLock) LoadScene();
        }
    }

    public void LoadScene()
    {
        loadLock = true;
        SceneManager.LoadScene("MainMenu");
    }
}
