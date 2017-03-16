using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : Character
{
	
	private static Player instance;

	public static Player Instance {
		get { 
			if (instance == null) {
				instance = GameObject.FindObjectOfType <Player> ();
			}
			return instance;
		}
	}
    [SerializeField]
    private GameObject startPoint;
	[SerializeField]
	private Transform[] groundPoints;
	[SerializeField]
	private float groundRadius;
	[SerializeField]
	private LayerMask WhatIsGround;
	//private bool IsGrounded;
	//private bool jump;
	[SerializeField]
	private float jumpForce = 200;
	[SerializeField]
	private bool airControl;
	public float forwardSpeed = 20;
    static public int Score;
    public int ThrowRemains = 0;
    public bool Freeze= false;
    public bool Vulnerable = true;
    public GameObject GameCanvas;
    public AudioClip stepSound;
    public AudioClip hurtSound;
    public AudioClip attackSound;
    public AudioClip daggerSound;
    public AudioClip slideSound;
    private bool loadLock;

    public Rigidbody2D MyRigidbody { get; set; }

	public bool Falling;
	public float YSpeed;

	public bool Jump { get; set; }

	public bool Slide { get; set; }
	[SerializeField]
	public bool OnGround { get; set; }

	public bool IsFalling {
		get {
			this.YSpeed = MyRigidbody.velocity.y;
			this.Falling = !(YSpeed < 0.0001 && YSpeed > -0.0001);
				return Falling;
		}
	}
	public override void Start ()
	{
		base.Start ();
        loadLock = false;
		MyRigidbody = GetComponent<Rigidbody2D> ();
	}

	void Update ()
	{
		HanldeInput ();
	}


	void FixedUpdate ()
	{
		float horizontal = Input.GetAxis ("Horizontal");
		OnGround = IsGrounded () && !MyAnimator.GetCurrentAnimatorStateInfo(1).IsTag("JumpUp");
		HandleMovement (horizontal);
		Flip (horizontal);
		//HandleAttacks ();
		HandleLayers ();
        //ResetValue ();
        if (IsDead)
        {
                MyAnimator.SetLayerWeight(1, 0);
                MyAnimator.SetTrigger("die");
                Freeze = true;
        }
	}


	private void HandleMovement (float horizontal)
	{
		if (IsFalling && gameObject.layer!=9) {
			gameObject.layer = 10;
			MyAnimator.SetBool ("land", true);
		}
        //freeze
        if (Freeze)
        {
            MyRigidbody.velocity = new Vector2(0, MyRigidbody.velocity.y);
        }
        //run
        else if (!Attack && !Slide) {
			MyRigidbody.velocity = new Vector2 (horizontal * movementSpeed, MyRigidbody.velocity.y);
		}
        //slide
        else if (!Attack && Slide)
        {
            GetComponent<BoxCollider2D>().size = new Vector2(2, 3.5f);
            GetComponent<BoxCollider2D>().offset = new Vector2(-0.45f, -0.665f);
            MyRigidbody.velocity = new Vector2(horizontal * 2*movementSpeed, MyRigidbody.velocity.y);
        }
        //jump
        if (OnGround && Jump) {
			if (this.MyAnimator.GetCurrentAnimatorStateInfo (0).IsTag ("idle")) {
				MyRigidbody.velocity = (new Vector2 (0, 50 * (jumpForce / MyRigidbody.mass)));
			} else if (this.MyAnimator.GetCurrentAnimatorStateInfo (0).IsTag ("run")) {
				MyRigidbody.velocity = (new Vector2 (horizontal * movementSpeed, 50 * (jumpForce / MyRigidbody.mass)));
			} 
		}
		this.MyAnimator.SetFloat ("speed", Mathf.Abs (horizontal));
	}
    
	private void HanldeInput ()
	{
        if (PauseMenu.Paused == false)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) && OnGround)
            {
                MyAnimator.SetTrigger("slide");
                /*attack = true;
                jumpAttack = true;*/
            }
            if (Input.GetKeyDown(KeyCode.UpArrow) && !IsFalling)
            {
                MyAnimator.SetTrigger("jump");


            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Vulnerable = false;
                MyAnimator.SetTrigger("attack");
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                MyAnimator.SetTrigger("throw");

            }
        }
	}

	private void Flip (float horizontal)
	{
		if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight) {
			ChangeDirection ();
		}
	}
	
	private bool IsGrounded ()
	{
		if (MyRigidbody.velocity.y <= 0) {
			foreach (Transform point in groundPoints) {
				Collider2D[] colliders = Physics2D.OverlapCircleAll (point.position, groundRadius, WhatIsGround);
			
				for (int i = 0; i < colliders.Length; i++) {
					if (colliders [i].gameObject != gameObject) {
                        GetComponent<BoxCollider2D>().size = new Vector2(2.2f, 4.4f);
                        GetComponent<BoxCollider2D>().offset = new Vector2(-0.35f, -0.22f);
                        return true;
					}
				}
			}
		}
		return false;
	}

	private void HandleLayers ()
	{
		if (!OnGround)
			MyAnimator.SetLayerWeight (1, 1);
		else
			MyAnimator.SetLayerWeight (1, 0);
	}


	public override void ThrowStone ( int value){
        if (ThrowRemains == 0)
        {
            throwstone = (GameObject)Resources.Load("dagger");
            throwSound = daggerSound;
        }

        if (!OnGround && value == 1 || OnGround && value == 0){
			base.ThrowStone (value);
		}
        if(ThrowRemains > 0) ThrowRemains -= 1;
	}


	public override IEnumerator TakeDamage(){
        if (Vulnerable)
        {
            Health -= 10;// we can change later
            if (!IsDead)
            {
                if (hurtSound)
                {
                    AudioPlayer.PlayOneShot(hurtSound);
                }
                MyAnimator.SetTrigger("damage");
            }
            else
            {
                MyAnimator.SetLayerWeight(1, 0);
                MyAnimator.SetTrigger("die");
                Freeze = true;
            }
        }
            yield return null;
	}
	public override bool IsDead {
		get{ 
			return Health <= 0;
		}
	}

    public override void OnCollisionEnter2D(Collision2D other)
    {
        base.OnCollisionEnter2D(other);
    }

    public void RunningSound()
    {
        if (stepSound && !Falling)
        {
            AudioPlayer.PlayOneShot(stepSound);
        }
    }

    public void AttackSound()
    {
        if (attackSound)
        {
            AudioPlayer.PlayOneShot(attackSound);
        }
    }
    public void SlideSound()
    {
        if (slideSound)
        {
            AudioPlayer.PlayOneShot(slideSound);
        }
    }
    public override void OnBecameInvisible()
    {
        base.OnBecameInvisible();
    }

    public void Over()
    {
        PlayerPrefs.SetString("currentscene", SceneManager.GetActiveScene().name);
        if (!loadLock)
        {
            loadLock = true;
            SceneManager.LoadScene("GameOver");
        }
    }

}






    /*private void ResetValue(){
    attack = false;
    slide = false;
    jump = false;
    jumpAttack = false;
} */

    /*if (myRidgidbody.velocity.y < 0)
	myAnimator.SetBool ("land", true);
	if (!this.myAnimator.GetCurrentAnimatorStateInfo (0).IsTag ("Attack") && (IsGrounded ||airControl)) {
		myRidgidbody.velocity = new Vector2 (horizontal*movementSpeed, myRidgidbody.velocity.y);	
		//Debug.Log (IsGrounded);
	}
	if (IsGrounded && jump) {

		myAnimator.SetTrigger ("jump");

		if (this.myAnimator.GetCurrentAnimatorStateInfo (0).IsTag ("idle")) {
			myRidgidbody.velocity = (new Vector2 (0, 50 * (jumpForce / myRidgidbody.mass)));
		} else if (this.myAnimator.GetCurrentAnimatorStateInfo (0).IsTag ("run") || jumpAttack) {
			myRidgidbody.velocity = (new Vector2 (horizontal * movementSpeed, 50 * (jumpForce / myRidgidbody.mass)));
		} 
	}


	if (slide && !this.myAnimator.GetCurrentAnimatorStateInfo (0).IsTag ("Slide")) {
		myAnimator.SetTrigger ("slide");

		///} else if (!this.myAnimator.GetCurrentAnimatorStateInfo (0).IsTag("Slide")) {
		///	myAnimator.SetBool ("slide", false);
	}
	myAnimator.SetFloat ("speed", Mathf.Abs(horizontal));// link run mode with idle mode
	///Debug.Log (Mathf.Abs (horizontal));*/




    /*private void HandleAttacks(){

	if (attack && IsGrounded && !this.myAnimator.GetCurrentAnimatorStateInfo (0).IsTag ("Attack")) {
		myAnimator.SetTrigger ("attack");
		myRidgidbody.velocity = Vector2.zero;
	}
	if (jumpAttack && !IsGrounded && !this.myAnimator.GetCurrentAnimatorStateInfo (1).IsName ("Jump_Attack")){
		myAnimator.SetBool ("jumpAttack", true);
	}
	if (!jumpAttack && !this.myAnimator.GetCurrentAnimatorStateInfo (1).IsName ("Jump_Attack")) {
		myAnimator.SetBool ("jumpAttack", false);
	}
}*/



