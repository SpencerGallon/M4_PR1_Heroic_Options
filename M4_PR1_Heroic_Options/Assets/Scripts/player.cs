using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public Animator playerAnim;
	public Rigidbody _rb;
	public float w_speed, wb_speed, olw_speed, rn_speed, ro_speed;
	public bool walking;
	public Transform playerTrans;
    //public float jumpVelocity = 5f;
    public float distanceToGround = 0.1f;
    public LayerMask groundLayer;
    public GameObject bullet;
    public float bulletSpeed = 100f;
    //public delegate void JumpingEvent();
    //public event JumpingEvent playerJump;
    

    private CapsuleCollider _col;
    private GameBehavior _gameManager;
	
	void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<CapsuleCollider>();
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameBehavior>();
    }

	void FixedUpdate()
    {
		if(Input.GetKey(KeyCode.W))
        {
			_rb.velocity = transform.forward * w_speed * Time.deltaTime;
		}
		if(Input.GetKey(KeyCode.S))
        {
			_rb.velocity = -transform.forward * wb_speed * Time.deltaTime;
		}
	}
	void Update()
    {
		if(Input.GetKeyDown(KeyCode.W))
        {
			playerAnim.SetTrigger("walk");
			playerAnim.ResetTrigger("idle");
			walking = true;
		}

		if(Input.GetKeyUp(KeyCode.W))
        {
			playerAnim.ResetTrigger("walk");
			playerAnim.SetTrigger("idle");
			walking = false;
		}

		if(Input.GetKeyDown(KeyCode.S))
        {
			playerAnim.SetTrigger("walkback");
			playerAnim.ResetTrigger("idle");
		}

		if(Input.GetKeyUp(KeyCode.S))
        {
			playerAnim.ResetTrigger("walkback");
			playerAnim.SetTrigger("idle");
		}

		if(Input.GetKey(KeyCode.A)){
			playerTrans.Rotate(0, -ro_speed * Time.deltaTime, 0);
		}

		if(Input.GetKey(KeyCode.D)){
			playerTrans.Rotate(0, ro_speed * Time.deltaTime, 0);
		}

		if(walking == true){				
			if(Input.GetKeyDown(KeyCode.LeftShift))
            {
				w_speed = w_speed + rn_speed;
				playerAnim.SetTrigger("run");
				playerAnim.ResetTrigger("walk");
			}
			if(Input.GetKeyUp(KeyCode.LeftShift))
            {
				w_speed = olw_speed;
				playerAnim.ResetTrigger("run");
				playerAnim.SetTrigger("walk");
			}
		}

    //    if(IsGrounded() && Input.GetKeyDown(KeyCode.Space))
     //   {
     //       _rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
     //       playerJump();
    //    }

        if (Input.GetMouseButtonDown(0))
		{
			playerAnim.SetTrigger("throw");
			playerAnim.ResetTrigger("run");
			playerAnim.ResetTrigger("walk");
			playerAnim.ResetTrigger("idle");
			GameObject newBullet = Instantiate(bullet, transform.TransformPoint(new Vector3(3/2, 0, 2)), transform.rotation);

			Rigidbody bulletRB = newBullet.GetComponent<Rigidbody>();

			bulletRB.velocity = transform.forward * bulletSpeed;
		}

	}

//	private bool IsGrounded()
//   {
//        Vector3 capsuleBottom = new Vector3(_col.bounds.center.x, _col.bounds.min.y, _col.bounds.center.z);

//        bool grounded = Physics.CheckCapsule(_col.bounds.center, capsuleBottom, distanceToGround, groundLayer, QueryTriggerInteraction.Ignore);

//        return grounded;
//    }

    void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.CompareTag("Enemy"))
		{
			Debug.Log("Whack");
			_gameManager.HP -= 1;
		}
	}
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("health"))
		{
			// Perform healing logic here
			Debug.Log("Player is healed!");
			_gameManager.HP += 1;
			// Disable the health object so it cannot be collected again
			other.gameObject.SetActive(false);
		}
	}
}