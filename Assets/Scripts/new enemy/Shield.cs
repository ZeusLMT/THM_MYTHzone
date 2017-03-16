using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {
    [SerializeField]
    private AudioClip shieldSound;
    private AudioSource AudioPlayer;
    void Start()
    {
        AudioPlayer = gameObject.AddComponent<AudioSource>();
        if (shieldSound) AudioPlayer.PlayOneShot(shieldSound);
    }
    void OnCollisionEnter2D(Collision2D Other)
    {
        if (Other.gameObject.tag == "PlayerKnife" || Other.gameObject.tag == "KnifeAttack") {
            Destroy(Other.gameObject);
            Debug.Log("destroy");
        }
    }

    void OnTriggerEnter2D(Collider2D Other)
    {
        if (Other.gameObject.tag == "PlayerKnife" || Other.gameObject.tag == "KnifeAttack")
        {
            Destroy(Other.gameObject);
            Debug.Log("destroyed");
        }
    }
}
