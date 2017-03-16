using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2shoot : MonoBehaviour {

	public GameObject Projectile;


	// Use this for initialization
	void Start () {
		Invoke ("FireEnemyBullet", 1f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	/*void FireEnemyBullet (){
		GameObject player = GameObject.Find ("Player");
		if (player != null) {
			GameObject bullet = (GameObject)Instantiate (Projectile);
			bullet.transform.position = transform.position;
			Vector2 direction = player.transform.position - bullet.transform.position;
			bullet.GetComponent<Projectile> ().SetDirection (direction);
		}
	}*/
}
