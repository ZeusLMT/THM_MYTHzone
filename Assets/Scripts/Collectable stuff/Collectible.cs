using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour {
    public int AddScore;
    public int AddHP;
    [SerializeField]
    private bool Explodable;
    public Explode ExplodeScript;
    [SerializeField]
    private AudioClip collectSound;

	// Use this for initialization
	void Start () {
        if (Explodable) ExplodeScript = gameObject.GetComponent<Explode>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter2D (Collider2D target){
        if (target.gameObject.tag == "Player")
        {
            if (collectSound)
            {
                Player.Instance.AudioPlayer.PlayOneShot(collectSound);
            }
            OnCollect (target.gameObject);
            if (Player.Instance.Health <= (100 - AddHP)) Player.Instance.Health += AddHP;
            else Player.Instance.Health = 100;
            Player.Score += AddScore;
            if (gameObject.tag == "WaterSplash"){
                ExplodeScript.OnExplode();
            }
            else Destroy(gameObject);
        }
	}
	protected virtual void OnCollect(GameObject target){

	}
}
