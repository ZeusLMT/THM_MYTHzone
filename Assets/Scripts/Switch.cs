using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour {
	[SerializeField]
	private SpikeMoving spikeMoving;
	public Jail Spike;
	private Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();

	}

	// Update is called once per frame
	void Update () {

	}

	void OnCollisionStay2D(Collision2D target){
		animator.SetInteger ("AnimState", 1);
		Toggle(true);
		if(Spike != null) Spike.Escape ();
		}


	void OnCollisionExit2D(Collision2D target){


		animator.SetInteger ("AnimState", 2);
		Toggle(false);
		}


	public void Toggle(bool value){
		if (spikeMoving != null) {
			if (value) {
				spikeMoving.Open ();
			} else
				spikeMoving.Close ();
		}
	}

}
