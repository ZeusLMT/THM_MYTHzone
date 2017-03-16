using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFire : MonoBehaviour
{
    [SerializeField]
    private AudioClip shotSound;
    private AudioSource AudioPlayer;
    void Start()
    {
        AudioPlayer = gameObject.AddComponent<AudioSource>();
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
     public void Shot()
    {
        if (shotSound) AudioPlayer.PlayOneShot(shotSound);
    }

}
