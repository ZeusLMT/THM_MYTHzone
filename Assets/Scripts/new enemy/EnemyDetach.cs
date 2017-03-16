using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetach : MonoBehaviour {

	public GameObject projectile;
	protected bool inRange;
	public float attackDelay = 5f;



	void OnTriggerEnter2D ( Collider2D other){
		if (other.tag == "Player") {
			inRange = true;
			Debug.Log ("enter2d");

		}

	}
	void OnTriggerExit2D ( Collider2D other )
	{
		if (other.tag == "Player") {
			inRange = false;
			Debug.Log ("exit");
		}
	}
	void Start()
	{

		if (attackDelay != 0)
		{
			StartCoroutine(OnAttack());
		}
	}

	IEnumerator OnAttack()
	{
		yield return new WaitForSeconds(attackDelay);
		if ( inRange)
			OnShoot();
		
		StartCoroutine(OnAttack());
	}
	void OnShoot()
	{
		if (projectile != null) {
			//    var clone = Instantiate(projectile, transform.position, Quaternion.identity);
			//    Debug.Log("shoot");
			//}

			GameObject tmp = Instantiate (projectile, transform.position, Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject;
			tmp.GetComponent<Projectile> ().Initialize (Vector2.left);

			Debug.Log ("shoot");
		}
	}

}