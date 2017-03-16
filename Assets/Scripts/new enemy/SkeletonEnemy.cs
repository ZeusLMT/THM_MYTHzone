using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonEnemy : MonoBehaviour
{
    private Player player;
    private Animator animator;
    public bool facingRight;
    [SerializeField]
    private float activeRange;
    [SerializeField]
    private float chaseThreshold;
    [SerializeField]
    private float attackThreshold;
    [SerializeField]
    private float speed;
    private Rigidbody2D MyRigidbody;
    [SerializeField]
    private Transform BasePosition;
    private float xDir;
    private bool Wall;
    private float baseDirection;
    private int health = 40;


    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        MyRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        LookAtTarget();
    }
    private void LookAtTarget()
    {

        if (player != null)
        {
            xDir = player.transform.position.x - transform.position.x;
            float distance = Vector2.Distance(player.transform.position, transform.position);

            if (distance < activeRange)
            {
                if (xDir < 0 && facingRight || xDir > 0 && !facingRight)
                {
                    if (Mathf.Abs(xDir) >= 10) ChangeDirection();
                    Wall = false;
                }
                if (distance > chaseThreshold && distance < activeRange || Wall)
                {
                    animator.SetInteger("AnimState", 0);
                }
                else if (distance <= chaseThreshold && distance > attackThreshold)
                {
                    animator.SetInteger("AnimState", 2);
                    animator.SetTrigger("Run");
                    transform.Translate(GetDirection() * (speed * Time.deltaTime), Space.World);
                }
                else if (distance <= attackThreshold)
                {
                    animator.SetInteger("AnimState", 1);
                    MyRigidbody.velocity = new Vector2(0, 0);
                }
            }

            else
            {
                baseDirection = gameObject.transform.position.x - BasePosition.position.x;
                if ((baseDirection > 0 && facingRight) || (baseDirection < 0 && !facingRight))
                {
                    ChangeDirection();
                }
                if (Mathf.Abs(gameObject.transform.position.x - BasePosition.position.x) > 0.5)
                {
                    animator.SetInteger("AnimState", 2);
                    animator.SetTrigger("Run");
                    gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, BasePosition.position, speed * Time.deltaTime);
                }
                else if (Mathf.Abs(gameObject.transform.position.x - BasePosition.position.x) <= 0.5)
                {
                    animator.ResetTrigger("Run");
                    animator.SetInteger("AnimState", 0);
                }
            }
        }
    }
    public void ChangeDirection()
    {
        facingRight = !facingRight;
        //transform.localScale = new Vector3 (transform.localScale.x * -1, 1, 1);
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

    }

    public Vector2 GetDirection()
    {
        return facingRight ? Vector2.right : Vector2.left;


    }
    void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag == "Platform")
        {
            Wall = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {

        if (other.gameObject.tag == "Platform")
        {
            Wall = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
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
}
