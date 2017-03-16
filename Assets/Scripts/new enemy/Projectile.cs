using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody2D))]
public class Projectile : MonoBehaviour {
	
	[SerializeField]
	protected float speed;
	protected Rigidbody2D myRigidbody;
	protected Vector2 direction;
    private bool flying = true;
    private Animator animator;
    // Use this for initialization
    void Start () {
		myRigidbody = GetComponent<Rigidbody2D> ();
        animator = GetComponent<Animator>();
    }

	private void Awake () {
		Physics2D.IgnoreCollision (GetComponent <Collider2D >(), Enemy2.FindObjectOfType<BoxCollider2D>(), true);
		Physics2D.IgnoreCollision (GetComponent <Collider2D >(), EnemyDetach.FindObjectOfType<BoxCollider2D>(), true);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Shield.FindObjectOfType<BoxCollider2D>(), true);

    }



	void OnBecameInvisible (){
		Destroy (gameObject);

	}
	public void Initialize (Vector2 direction){
		this.direction = direction;
	}

	void FixedUpdate () {
		if(flying) myRigidbody.velocity = direction * speed;
    }


	void OnCollisionEnter2D ( Collision2D target){
		if (target.gameObject.name == " Commander" || target.gameObject.name == "CommanderShield") {	
			Physics2D.IgnoreCollision (GetComponent<Collider2D> (), target.collider, true);
		}
		

		if (this.gameObject.name == "PurpleBullet(Clone)" )
        {
            animator.SetTrigger("Explode");
            flying = false;
            this.myRigidbody.velocity = direction * 0;
            Destroy (gameObject, 0.2f);
            return;

		} else
			Destroy (gameObject);


        if (target.gameObject.tag == "PlayerKnife")
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), target.collider, false);
        } else
        {
            Destroy(gameObject);
        }
	}
}

//float speed;
	/*Vector2 _direction;
	bool isReady;
	// set default values in Awake function
	void Awake(){
		speed = 10f;
		isReady = false;

	}*/
// function to set the bullet's direction
/*public void SetDirection ( Vector2 direction){
		/*_direction = direction.normalized;
		isReady = true;
	}*/
/*void Update () {
	if (isReady) {
			Vector2 position = transform.position;
			position += _direction * speed * Time.deltaTime;
			transform.position = position;
			Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
			Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));
			if ((transform.position.x < min.x) || (transform.position.x > max.x) ||
			    (transform.position.y < min.y) || (transform.position.y > max.y)) {
				Destroy (gameObject);
			}
}
		}*/

