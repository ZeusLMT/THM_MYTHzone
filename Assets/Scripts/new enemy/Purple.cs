using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purple : MonoBehaviour {
    private Player player;
    private Animator animator;
    public bool facingRight;
    [SerializeField]
    private float activeRange;
    [SerializeField]
    private float attackThreshold;
    private float xDir;
    private int health = 40;
    private bool sleeping=true;
    [SerializeField]
    private AudioClip attackSound;
    private AudioSource AudioPlayer;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        AudioPlayer = gameObject.AddComponent<AudioSource>();
    }
    
    void Update()
    {
        LookAtTarget();
    }
    private void LookAtTarget()
    {

        if (player != null)
        {
            xDir = player.transform.position.x - transform.position.x;
            float distance = Mathf.Abs(player.transform.position.x - transform.position.x);
            if (distance <= activeRange + 10)
            {
                if (sleeping)
                {
                    animator.SetTrigger("awake");
                    sleeping = false;
                }
                if (xDir < 0 && facingRight || xDir > 0 && !facingRight)
                {
                    ChangeDirection();
                }

                if (distance <= activeRange && distance > attackThreshold)
                {
                    animator.SetTrigger("idle");
                }
                else if (distance <= attackThreshold)
                {
                    animator.ResetTrigger("idle");
                    animator.SetTrigger("attack");
                }
            }

            else
            {
                animator.SetTrigger("rest");
                sleeping = true;
            }
        }
    }
    public void ChangeDirection()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

    }

    public Vector2 GetDirection()
    {
        return facingRight ? Vector2.right : Vector2.left;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PlayerKnife" || other.gameObject.tag == "KnifeAttack")
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

    private void AttackSound()
    {

        if (attackSound)
        {
            AudioPlayer.PlayOneShot(attackSound, 0.1f);
        }
    }
}
