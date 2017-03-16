using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {
    void Start() { 
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
