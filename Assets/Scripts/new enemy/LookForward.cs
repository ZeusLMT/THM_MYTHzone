using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookForward : MonoBehaviour
{
	public Transform sightStart, sightEnd;
	public bool needsCollision = true;
	private bool collision = false ;


	void Start () {

	}
	void Update () {
		collision = Physics2D.Linecast(sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer ("Platform"));
		Debug.DrawLine (sightStart.position, sightEnd.position, Color.green);

		if(collision == needsCollision)
			this.transform.localScale = new Vector3(- (transform.localScale.x), transform.localScale.y, 1);
	}
}


