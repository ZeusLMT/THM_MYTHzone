using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleRunning : MonoBehaviour
{
    public int AddScore;
    [SerializeField]
    private AudioClip collectSound;

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag == "Player")
        {
            if (collectSound)
            {
                AudioSource.PlayClipAtPoint(collectSound, transform.position);
            }
            Player.Score += AddScore;
            Destroy(gameObject);
        }
    }
}
