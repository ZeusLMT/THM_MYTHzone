using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour {

	public float speed = 10f;
	private Rigidbody2D body2D;
    [SerializeField]
	protected int health =30;
	private Animator animator;
    private float direction;
    public bool haveShield = false;
	//private Explode explode;
    //private bool facingRight = false;
	// Use this for initialization
	void Start () {
		body2D = GetComponent <Rigidbody2D> ();
		animator = GetComponent<Animator>();
		//explode= GameObject.Find ("Player").GetComponent< Explode >();
    }
	
	// Update is called once per frame
	void Update () {
        if (this.animator.GetCurrentAnimatorStateInfo(0).IsTag("Walking"))
        {
            body2D.velocity = new Vector2(transform.localScale.x, 0) * speed;
        }
        if (this.animator.GetCurrentAnimatorStateInfo(0).IsTag("Damage"))
        {
            body2D.velocity = new Vector2(transform.localScale.x, 0) * 1.5f*speed*direction* Mathf.Sign(body2D.transform.localScale.x);
        }
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.tag == "PlayerKnife" || other.gameObject.tag == "KnifeAttack" ) && !haveShield)
        {
            health -= 10;
            if (health > 0) animator.SetTrigger("damage");
            direction = - Mathf.Sign(other.gameObject.transform.position.x - body2D.transform.position.x) ;
            if (other.gameObject.tag != "KnifeAttack") Destroy(other.gameObject);
        }
        if (other.gameObject.name == "FireBall(Clone)")
        {
            health -= 30;
            if (health > 0) animator.SetTrigger("damage");
            direction = -Mathf.Sign(other.gameObject.transform.position.x - body2D.transform.position.x);
            Destroy(other.gameObject);
        }
        
        if(health <= 0)
        {
            animator.SetTrigger("die");
            Destroy(gameObject, 1f);
        }
    }
}
