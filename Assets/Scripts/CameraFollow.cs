using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
	[SerializeField]
	private float xMax;
	[SerializeField]
	private float yMax;
	[SerializeField]
	private float xMin;
	[SerializeField]
	private float yMin;
    [SerializeField]
	private Transform target;

	void Start () {
		if(GameObject.Find("Player") != null) { target = GameObject.Find("Player").transform;
        }
	}
	
    void Update()
    {
    }
	// Update is called once per frame
	void LateUpdate () {
        if (target != null)
        {
            transform.position = new Vector3(Mathf.Clamp(target.position.x, xMin, xMax),
                Mathf.Clamp(target.position.y, yMin, yMax), transform.position.z);
        }
    }
}
