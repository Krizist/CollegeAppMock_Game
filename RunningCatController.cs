using UnityEngine;
using System.Collections;

public class RunningCatController : MonoBehaviour {

	public float maxSpeed = 10f;
	bool facingRight = true;

	Animator anim;

	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	float jumpForce = 700f;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("Ground", grounded);
		anim.SetFloat ("vSpeed", GetComponent<Rigidbody2D> ().velocity.y);

		float move = Input.GetAxis ("Horizontal");

		anim.SetFloat ("speed", Mathf.Abs(move));

		GetComponent<Rigidbody2D>().velocity = new Vector2 (move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

		if (move > 0 && !facingRight) 
			Flip();
		// Otherwise if the input is moving the player left and the player is facing right...
		else if (move < 0 && facingRight) 
			// ... flip the player.
			Flip();
	}

	void Update()
	{
		if(grounded && Input.GetKeyDown(KeyCode.Space))
		{
			anim.SetBool ("Ground", false);
			GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, jumpForce));  
		}
			
	}

	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
