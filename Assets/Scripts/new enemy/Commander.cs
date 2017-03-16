using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commander : MonoBehaviour
{
    private Animator animator;
    private float Delay;
    [SerializeField]
    private GameObject Shield;
    private bool ShieldActive=false;
    private MoveForward MFScript;
	private Player player;
	public bool facingLeft;
	private Rigidbody2D body;
	[SerializeField]
	private float activeRange;
	public GameObject projectile;
	public float attackDelay;
	private bool attack;
	private float nextFire = 1;
	private float fireTime = 1;
    private int BulletPos;
    [SerializeField]
	private PlatformLayer instance;
    void Start()
    {
		
        animator = GetComponent<Animator>();
        Shield = GameObject.Find("CommanderShield");
        Delay = Random.Range(1, 5);
        MFScript = GetComponent<MoveForward>();
		body = GetComponent<Rigidbody2D> ();
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		//instance = GetComponent<PlatformLayer> ();
    }

    void Update()
    {

        if (transform.localScale.x < 0) facingLeft = true;
        else facingLeft = false;
        //if(animator.GetCurrentAnimatorStateInfo(0).IsTag("Damage"))
        Delay -= Time.deltaTime;
        if(Delay <= 0)
        {
            if (ShieldActive) ShieldOff();
            else ShieldOn();
            Delay = Random.Range(1, 5);
        }
		//attack = false;
		AttackTarget ();
    }

    private void ShieldOn()
    {
        animator.SetTrigger("ShieldOn");
        Shield.SetActive(true);
        ShieldActive = true;
        MFScript.haveShield = true;
    }

    private void ShieldOff()
    {
        animator.SetTrigger("ShieldOff");
        Shield.SetActive(false);
        ShieldActive = false;
        MFScript.haveShield = false;
    }
    private void AttackTarget()
    {
        if (player != null)
        {
            var xDir = player.transform.position.x - transform.position.x;
            float distance = Mathf.Abs(xDir);

			if (distance < activeRange && instance.PlayerActive == true )
            {
                body.velocity = new Vector2(0, 0);
                if (xDir < 0 && !facingLeft || xDir > 0 && facingLeft)
                {
                    ChangeDirection();
                }
                animator.SetTrigger("Attack");

            }
            else
                animator.SetTrigger("Walk");
        }
    }

	private void KillTarget(){

		if (projectile != null && Time.time>= nextFire) {
			nextFire = Time.time + fireTime;

			var tmp = Instantiate(projectile, new Vector3(transform.position.x - 10, transform.position.y -25 + 30* Random.Range(0, 2), transform.position.z), Quaternion.Euler (new Vector3 (0, 0, 0)));
			if (facingLeft)
			{
				tmp.GetComponent<Projectile>().Initialize(Vector2.left);
			}
			else tmp.GetComponent<Projectile>().Initialize(Vector2.right);
		}
	}

		public void ChangeDirection()
		{
			facingLeft = !facingLeft;
			//transform.localScale = new Vector3 (transform.localScale.x * -1, 1, 1);
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
		}

}
