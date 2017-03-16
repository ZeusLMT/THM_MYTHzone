using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap1Switch : MonoBehaviour {
    public GameObject MovingPlatform;
    [SerializeField]
    private AudioClip activateSound;
    private AudioSource AudioPlayer;
    
    void Start()
    {
        AudioPlayer = gameObject.AddComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D Other)
    {
        if (Other.gameObject.name == "Crate" || Other.gameObject.name == "Player")
        {
            if (activateSound) AudioPlayer.PlayOneShot(activateSound, 1);
        }
    }
    void OnTriggerStay2D(Collider2D Other)
    {
        if(Other.gameObject.name == "Crate" || Other.gameObject.name == "Player")
        {
            MovingPlatform.GetComponent<PlatformMoving>().enabled = true;
        }
    }
    void OnTriggerExit2D (Collider2D Other)
    {
        MovingPlatform.GetComponent<PlatformMoving>().enabled = false;
    }
}
