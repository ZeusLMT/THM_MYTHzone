using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderEnemy : MonoBehaviour {

	private float Timer;
	private float delay;
	[SerializeField]
	private float AttackTime;
	public float delayMax;
	public float delayMin;
	private Animator animator;


	void Start () {
		animator = GetComponent<Animator> ();
	
		StartCoroutine(SpiderGo());
		delay = Random.Range(delayMin, delayMax);
		Timer = 0f;

	}


	IEnumerator SpiderGo(){
	yield return new WaitForSeconds (delay);
		animator.SetInteger ("AnimState", 0);


		StartCoroutine (SpiderGo ());

	}
	// Update is called once per frame
	void Update () {
			Timer += Time.deltaTime;

		if (Timer > AttackTime) {
			animator.SetInteger ("AnimState", 1);
			Timer = 0f;


		}		
		
	}

}