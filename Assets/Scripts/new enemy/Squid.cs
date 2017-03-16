using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squid : MonoBehaviour {
    [SerializeField]
    private float jumpForce = 30;
    protected int health = 50;
    private Animator animator;
    private Rigidbody2D SquidBody;
    private float FlyDelay = 5;
    private float JumpDelay = 3;
    private Rigidbody2D Player;
    private float PlayerDirection;
    private float Distance;
    [SerializeField]
    private float Range;
    private bool FaceRight = false;
    [SerializeField]
    private GameObject Splash;
    private AudioSource AudioPlayer;
    public AudioClip attackSound;
    public AudioClip splashSound;



    private bool up = false, down = false, fly = false;

    void Start()
    {
        AudioPlayer = gameObject.AddComponent<AudioSource>();
        AudioPlayer.volume = 0.3f;
        animator = GetComponent<Animator>();
        SquidBody = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        Splash = transform.parent.GetChild(2).gameObject;
        SquidBody.gravityScale = 50;
        SquidBody.mass = 30;
        up = true;
    }

    void FixedUpdate()
    {
        Tracking();
        if (Distance < Range)
        {
            FlyDelay -= Time.deltaTime;
            JumpDelay -= Time.deltaTime;
            if (up && JumpDelay <= 0)
            {
                Jump();

            }
            if (fly && SquidBody.velocity.y <= 5)
            {
                Attack();
            }
            if (down && FlyDelay <= 0)
            {
                Fall();
            }
        }
    }


    private void Jump() {
        SquidBody.velocity = (new Vector2(0, 10 * jumpForce));
        Splash.SetActive(true);
        Splash.GetComponent<Animator>().SetTrigger("Splash");
        SplashSound();
        GetComponent<CircleCollider2D>().enabled = false;
        up = false;
        down = false;
        fly = true;
    }

    private void Attack()
    {
        animator.ResetTrigger("Fall");
        animator.SetTrigger("Attack");
        if (SquidBody.velocity.y <= 0)
        {
            SquidBody.gravityScale = 0;
            SquidBody.velocity = (new Vector2(0, 0));
            up = false;
            down = true;
            fly = false;
            Splash.SetActive(false);
        }
    }
    private void Fall()
    {
        animator.ResetTrigger("Attack");
        animator.SetTrigger("Fall");
        SquidBody.gravityScale = 50;
        GetComponent<CircleCollider2D>().enabled = true;
        up = true;
        down = false;
        fly = false;
        JumpDelay = 3;
        FlyDelay = 5;
    }

    private void Tracking()
    {
        if (Player != null)
        {
            PlayerDirection = Mathf.Sign(SquidBody.transform.position.x - Player.transform.position.x);
            Distance = Mathf.Abs(Player.transform.position.x - transform.position.x);
            if ((PlayerDirection == 1 && FaceRight) || (PlayerDirection == -1 && !FaceRight))
            {
                SquidBody.transform.localScale = new Vector2(SquidBody.transform.localScale.x * -1, SquidBody.transform.localScale.y);
                FaceRight = !FaceRight;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.tag == "PlayerKnife" || other.gameObject.tag == "KnifeAttack"))
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
        if (SquidBody.velocity.y == 0) animator.SetTrigger("Attack");
    }

    public void SplashSound()
    {
        if (splashSound)
        {
            AudioPlayer.PlayOneShot(splashSound);
        }
    }

    public void AttackSound() {
        if (attackSound)
        {
            AudioPlayer.PlayOneShot(attackSound);
        }
    }
}
