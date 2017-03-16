using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class James : MonoBehaviour {
    [SerializeField]
    private AudioClip meetSound;
    private AudioSource AudioPlayer;
    private Rigidbody2D JamesBody;
    private bool Trigger;
    private float delay = 0;
    private bool loadLock;

	// Use this for initialization
	void Start () {
        AudioPlayer = gameObject.AddComponent<AudioSource>();
        JamesBody = GetComponent<Rigidbody2D>();
        loadLock = false;
	}
	
    void Update()
    {
        if (Trigger)
        {
            delay += Time.deltaTime;
            if(delay >= 3 && !loadLock)
            {
                TheEnd();
            }
        }
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Trigger = true;
            JamesBody.velocity = new Vector2(100, 0);
            if(meetSound) AudioPlayer.PlayOneShot(meetSound);
        }
    }
    void TheEnd()
    {
        loadLock = false;
        SceneManager.LoadScene("TheEnd");
    }
}
