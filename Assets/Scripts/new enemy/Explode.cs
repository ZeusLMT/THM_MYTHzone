using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Explode : MonoBehaviour {
	public BodyPart bodyPart;
	private int totalParts =6;
    public bool Active = true;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D target){
		if ((target.gameObject.tag == "Deadly" && Active)||
			(gameObject.tag ==" WaterSplash" && target.gameObject.tag == "Player" && Active) ){
			OnExplode();
		}
	
			

	}

	/*void OnCollisionEnter2D(Collision2D target){
		if (target.gameObject.tag == "Deadly") {
			OnExplode();
		}

	}*/

	public void OnExplode(){
		Destroy (gameObject);

		var t = transform;

		for (int i = 0; i < totalParts; i++) {
			t.TransformPoint (0, -100, 0);
			BodyPart clone = Instantiate(bodyPart, t.position, Quaternion.identity) as BodyPart;
			clone.GetComponent <Rigidbody2D>().AddForce(Vector3.right * (Random.Range (-50, 50)));
			clone.GetComponent <Rigidbody2D>().AddForce(Vector3.up * Random.Range(100, 400));
		}

	}

}
