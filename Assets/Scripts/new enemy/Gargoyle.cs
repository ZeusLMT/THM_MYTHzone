using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gargoyle : MonoBehaviour {

	[SerializeField]
	private int health = 30;
	private Animator animator;
	private bool haveShield = false;
	public GameObject projectile;


	private float nextFire = 1;
	private float fireTime = 1;
	private int BulletPos;
	[SerializeField]
	private float activeRange;
	private Player player;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		animator = GetComponent<Animator>();
	}
	// Use this for initialization
	void OnTriggerEnter2D(Collider2D other)
	{
		if ((other.gameObject.tag == "PlayerKnife" || other.gameObject.tag == "KnifeAttack") && !haveShield)
		{
			health -= 10;
			if (health > 0) animator.SetTrigger("damage");
			if (other.gameObject.tag != "KnifeAttack") Destroy(other.gameObject);
		}
		if (other.gameObject.name == "FireBall(Clone)")
		{
			health -= 30;
			if (health > 0) animator.SetTrigger("damage");
			Destroy(other.gameObject);
		}

		if (health <= 0)
		{
			animator.SetTrigger("die");
			Destroy(gameObject, 1f);
		}
	}

	void Update (){
		AttackTarget ();
		}
	private void AttackTarget()
	{
		if (player != null)
		{
			var xDir = player.transform.position.x - transform.position.x;
			float distance = Mathf.Abs(xDir);


			if (distance < activeRange) {
				animator.SetTrigger ("Attack");


			} else {
				animator.SetTrigger ("Idle");

			}
		
		}
	}

	private void KillTarget(){

		if (projectile != null && Time.time>= nextFire) {
			nextFire = Time.time + fireTime;

			var tmp = Instantiate(projectile, new Vector3(transform.position.x - 10, transform.position.y -25 + 30* Random.Range(0, 2), transform.position.z), Quaternion.Euler (new Vector3 (0, 0, 0)));


				tmp.GetComponent<Projectile>().Initialize(Vector2.left);
		}
	}

}
