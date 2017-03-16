using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeMoving : MonoBehaviour {
	private Vector3 posA;
	private Vector3 posB;
    [SerializeField]
	private Transform childTransform;
	[SerializeField]
	private Transform transformB;
	[SerializeField]
	private float speed;
	public float closeDelay;
    private bool down;
    private bool up;

	// Use this for initialization
	void Start () {
        down = true;
        up = false;
		posA = childTransform.localPosition;
		posB = transformB.localPosition;
	}
	void Update() {
		if (down && gameObject.name == "Spikemoving") {
			childTransform.localPosition = Vector3.MoveTowards (childTransform.localPosition, posA, speed * Time.deltaTime * 20);
		} else if (down && gameObject.name == "MovingPlatformBoss")
			childTransform.localPosition = Vector3.MoveTowards (childTransform.localPosition, posA, speed * Time.deltaTime);
        else if (up)
        {
            childTransform.localPosition = Vector3.MoveTowards(childTransform.localPosition, posB, speed * Time.deltaTime);
		}
	}

	public void Open (){
        down = false;
        up = true;
	}
	public void Close (){
        up = false;
		StartCoroutine (CloseNow ());
	}
    private IEnumerator CloseNow(){
		yield return new WaitForSeconds(closeDelay);
        down = true;
    }
	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Player") {
			other.gameObject.layer = 9;
			other.transform.SetParent (childTransform);

		}
	}
	private void OnCollisionExit2D ( Collision2D other){
		other.transform.SetParent (null);
		other.gameObject.layer = 0;

	}
}


