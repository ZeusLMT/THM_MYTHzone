using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour {
    [SerializeField]
    private Player Player;
    [SerializeField]
    private EdgeCollider2D KnifeCollider;
	void Start () {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        KnifeCollider = GetComponent<EdgeCollider2D>();
    }

    void Update()
    {
        if(!KnifeCollider.enabled && Player != null)
        {
            Player.GetComponent<Explode>().Active = true;
            Player.Instance.Vulnerable = true;
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {

    }


}
