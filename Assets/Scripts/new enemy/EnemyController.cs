using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour {

	[SerializeField]
	private List<string> damageSources;
	protected int health =30;
	private Animator animator;
	private bool TakingDamage { get; set;}

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public  IEnumerator TakeDamage(){
		health -= 10;// we can change later

		if (health>0) {
			Debug.Log ("damage enemy1");
			animator.SetTrigger ("damage");

		} else {
			
			animator.SetTrigger ("die");
			Destroy (gameObject,1);

		}
		yield return null;
	}

	public virtual void OnCollisionEnter2D (Collision2D other){

		if (damageSources.Contains(other.gameObject.tag)) {
			Debug.Log ("TakeDamage");
			StartCoroutine (TakeDamage ());
			Destroy (other.gameObject);
		}
	}
}
