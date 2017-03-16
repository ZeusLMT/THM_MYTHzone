using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap1Trigger : MonoBehaviour {
    public GameObject Spikes;

    void OnTriggerEnter2D(Collider2D Other)
    {
        if (Other.gameObject.name == "Player")
        {
            Spikes.SetActive(true);
        }
    }
}
