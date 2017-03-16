using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformLayer : MonoBehaviour {
	public bool PlayerActive;
	// Use this for initialization

	void OnCollisionEnter2D(Collision2D other)
		{
			if (other.gameObject.tag == "Player") {
			PlayerActive = true;

			}
		}
	 void OnCollisionExit2D ( Collision2D other){
		PlayerActive = false;

		}
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerActive = true;

        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        PlayerActive = false;

    }
}
