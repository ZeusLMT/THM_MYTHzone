using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectVanish : MonoBehaviour {
    [SerializeField]
    private float delayTime;
    private float Timer = 0f;
    public float delayMax;
    public float delayMin;
    // Use this for initialization
    void Start () {
        delayTime = Random.Range(delayMin, delayMax);
        Timer = 0f;

	}
    void Update()
    {
        if (this.gameObject == null) return;
        else
        {
            Timer += Time.deltaTime;
            if (Timer >= delayTime)
            {
                DestroyObject(this.gameObject);

            }
        }
    }
}
