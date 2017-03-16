using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {
    private Animator animator;
    private float shieldDelay;
    [SerializeField]
    private float ActionSwitchDelay = 0;
    [SerializeField]
    private GameObject Shield;
    [SerializeField]
    private bool ShieldActive = false;
    private bool Moving;
    private Player player;
    public bool facingLeft;
    private Rigidbody2D body;
    [SerializeField]
    private float jumpForce = 300;
    public float forwardSpeed = 600;
    public float speed = 100;
    [SerializeField]
    protected int health = 300;
    private float direction;
    [SerializeField]
    public bool Vulnerable;
    [SerializeField]
    private GameObject Fire;
    private bool Dead;
    [SerializeField]
    private Collider2D feetCollider;
    [SerializeField]
    private Collider2D bodyTrigger;
    private AudioSource AudioPlayer;
    public AudioClip stepSound;
    public AudioClip hurtSound;
    public AudioClip landingSound;
    public AudioClip Laugh;



    void Start()
    {
        AudioPlayer = gameObject.AddComponent<AudioSource>();
        animator = GetComponent<Animator>();
        shieldDelay = Random.Range(1, 10);
        body = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Running();
        if (Laugh) AudioPlayer.PlayOneShot(Laugh);
    }
    void Update()
    {
        if (!Dead)
        {
            ActionSwitchDelay += Time.deltaTime;
            shieldDelay -= Time.deltaTime;

            if (transform.localScale.x < 0) facingLeft = true;
            if (shieldDelay <= 0 && !Dead)
            {
                if (ShieldActive) ShieldOff();
                else ShieldOn();
                shieldDelay = Random.Range(1, 10);
            }
            if (Moving)
            {
                Move();
            }
            if (ActionSwitchDelay < 4.05 && ActionSwitchDelay > 4)
            {
                Back2Idle();
            }
            if (ActionSwitchDelay >= 6 && player != null) ActionManager();
        }
    }
    private void ShieldOn()
    {
        Shield.SetActive(true);
        ShieldActive = true;
    }

    private void ShieldOff()
    {
        Shield.SetActive(false);
        ShieldActive = false;
    }
    
    private void ChangeDirection()
    {
        facingLeft = !facingLeft;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void ActionManager()
    {
        var Choice = Random.Range(1, 4);
        switch(Choice){
            case 1:
                {
                    Attacking();
                    break;
                }
            case 2:
                {
                    Running();
                    break;
                }
            case 3:
                {
                    Jumping();
                    break;
                }
        }
        ActionSwitchDelay = 0;
    }
    private void Attacking()
    {
        Vulnerable = false;
        animator.ResetTrigger("Idle");
        animator.SetTrigger("Attack");
        Moving = false;
        if (player != null)
        {
            var xDir = player.transform.position.x - transform.position.x;
            body.velocity = new Vector2(0, 0);
            if (xDir < 0 && !facingLeft || xDir > 0 && facingLeft)
            {
                ChangeDirection();
            }
            animator.SetTrigger("Attack");
        }
    }
    private void Running()
    {
        Vulnerable = false;
        animator.ResetTrigger("Idle");
        animator.SetTrigger("Run");
        Moving = true;
        Debug.Log("Runing");
    }

    private void Jumping()
    {
        Vulnerable = false;
        animator.ResetTrigger("Idle");
        animator.SetTrigger("Jump");
        Moving = false;
        Debug.Log("Jumping");
    }
    private void Back2Idle()
    {
        Moving = false;
        animator.ResetTrigger("Run");
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Jump");
        animator.SetTrigger("Idle");
        Vulnerable = true;
        if (player != null)
        {
            var xDir = player.transform.position.x - transform.position.x;
            body.velocity = new Vector2(0, 0);
            if (xDir < 0 && !facingLeft || xDir > 0 && facingLeft)
            {
                ChangeDirection();
            }
        }
    }
    void AddVelocity()
    {
        body.velocity = (new Vector2(transform.localScale.x * forwardSpeed, jumpForce));
    }
    void Freeze()
    {
        body.velocity = new Vector2(0, 0);
    }

    void SpawnFire()
    {
        if(player != null) Instantiate(Fire, new Vector2(player.transform.position.x + Random.Range(-30, 30), player.transform.position.y + Random.Range(-30, 30)), Quaternion.identity);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.tag == "PlayerKnife" || other.gameObject.tag == "KnifeAttack") && !ShieldActive && Vulnerable)
        {
            health -= 10;
            if (health > 0) animator.SetTrigger("damage");
            direction = -Mathf.Sign(other.gameObject.transform.position.x - body.transform.position.x);
            if (other.gameObject.tag != "KnifeAttack") Destroy(other.gameObject);
        }
        if (other.gameObject.name == "FireBall(Clone)")
        {
            health -= 30;
            if (health > 0) animator.SetTrigger("damage");
            direction = -Mathf.Sign(other.gameObject.transform.position.x - body.transform.position.x);
            Destroy(other.gameObject);
        }
        if (other.gameObject.name == "BossSmallPlatform")
        {
            Physics2D.IgnoreCollision(feetCollider, other, true);
        }

            if (health <= 0 && !Dead)
        {
            animator.SetTrigger("die");
            gameObject.tag = "Untagged";
            Dead = true;
        }
    }

    void Move()
    {
        if (this.animator.GetCurrentAnimatorStateInfo(0).IsTag("Walking"))
        {
            body.velocity = new Vector2(transform.localScale.x, 0) * speed;
        }
        if (this.animator.GetCurrentAnimatorStateInfo(0).IsTag("Damage"))
        {
            body.velocity = new Vector2(transform.localScale.x, 0) * 1.5f * speed * direction * Mathf.Sign(body.transform.localScale.x);
        }
    }
    public void RunningSound()
    {
        if (stepSound)
        {
            AudioPlayer.PlayOneShot(stepSound);
        }
    }


    public void LandingSound()
    {
        if (landingSound)
        {
            AudioPlayer.PlayOneShot(landingSound);
        }
    }

    public void HurtSound()
    {
        if (hurtSound)
        {
            AudioPlayer.PlayOneShot(hurtSound, 0.5f);
        }
    }

}
