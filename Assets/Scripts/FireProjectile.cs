using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : MonoBehaviour {
	public float shootDelay =0.5f;
	public GameObject projectilePrefab;
	private Animator animator;
	private Character player;
	private int canShoot;
	// Update is called once per frame

	void Start (){
		animator = GetComponent<Animator> ();
		player = GetComponent <Character> ();

	}
	void Update () {

		if (projectilePrefab != null) {

			if (Input.GetKeyDown (KeyCode.RightShift)) {
				
				animator.SetTrigger ("shoot");
			
			}

			/*if(shoot && timeElapsed > shootDelay){
				CreateProjectile(transform.position);
				timeElapsed = 0;
			}

			timeElapsed += Time.deltaTime;*/
		}

	}


	public void CreateProjectile(){
		
		if (player.facingRight)
		{
			var tmp = (GameObject)Instantiate(projectilePrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, -90)));
			tmp.GetComponent<weapon>().Initialize(Vector2.right);
			Debug.Log ("nem");
		}
		else
		{
			var tmp = (GameObject)Instantiate(projectilePrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, 90)));
			tmp.GetComponent<weapon>().Initialize(Vector2.left);
		}

	}

	}

