using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour {
	private Animator animator;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D ( Collider2D Target){
		if (Target.gameObject.name == "Player")
			animator.SetTrigger ("Show");
		Debug.Log ("heart");
	}
}
