using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour {
	[SerializeField]
	private SpikeMoving spikeMoving;
	public Jail Spike;
    private Vector3 originalPos;
    private bool MoveUp;
    [SerializeField]
    private AudioClip activeSound;
    private AudioSource AudioPlayer;
    private bool soundPlayed;

    // Use this for initialization
    void Start () {
        originalPos = transform.position;
        AudioPlayer = gameObject.AddComponent<AudioSource>();

    }

	// Update is called once per frame
	void Update () {
        if(MoveUp) this.transform.position = Vector3.MoveTowards(transform.position, originalPos, 20 * Time.deltaTime);
    }

	void OnCollisionStay2D(Collision2D target){
		if ( target.gameObject.name == "Player"){
        MoveUp = false;
        target.transform.SetParent(this.transform);
        if (!soundPlayed) ActiveSound();
        this.transform.position = Vector3.MoveTowards(transform.position, new Vector3(originalPos.x, originalPos.y - 10, originalPos.z), 20 * Time.deltaTime);
		Toggle(true);
		if(Spike != null) Spike.Escape ();
		}
	}


	void OnCollisionExit2D(Collision2D target){
		if ( target.gameObject.name == "Player")
        {MoveUp = true;
        target.transform.SetParent(null);
        Toggle(false);
            soundPlayed = false;
		}
	}


	public void Toggle(bool value){
		if (spikeMoving != null) {
			if (value) {
				spikeMoving.Open ();
			} else
				spikeMoving.Close ();
		}
	}
    public void ActiveSound()
    {
        if (activeSound) AudioPlayer.PlayOneShot(activeSound);
        soundPlayed = true;
    }
}
