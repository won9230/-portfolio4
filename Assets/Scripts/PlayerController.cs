using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : LivingEntity
{
	[SerializeField]private float moveSpeed = 5f;
	[SerializeField]private float jumpForce = 10f;
	private Animator anim;
	private Rigidbody2D rigi;
	private BoxCollider2D coll;
	private bool isJump;

	private void Start()
	{
		anim = GetComponent<Animator>();
		rigi = GetComponent<Rigidbody2D>();
		coll = GetComponent<BoxCollider2D>();
	}
	private void Update()
	{
		MovePlayer();
		Jump();
	}
	private void MovePlayer() //플레이어 이동
	{
		float h = Input.GetAxisRaw("Horizontal");
		rigi.AddForce(Vector2.right * h, ForceMode2D.Impulse);
		if (rigi.velocity.x > moveSpeed)
			rigi.velocity = new Vector2(moveSpeed,rigi.velocity.y);
		else if (rigi.velocity.x < -moveSpeed)
			rigi.velocity = new Vector2(-moveSpeed, rigi.velocity.y);
		if(h == 0)
		{
			rigi.velocity = new Vector2(rigi.velocity.normalized.x * 0.1f, rigi.velocity.y);
		}
		MoveAnim();
	}
	private void MoveAnim()//플레이어 이동 애니메이션
	{
		if (Input.GetAxisRaw("Horizontal") == 0 && !isJump)
		{
			anim.SetBool("isMove",false);
		}
		else
		{
			anim.SetBool("isMove", true);
		}
	}
	public void Jump()//플레이어 점프
	{
		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			rigi.AddForce(Vector2.up * jumpForce,ForceMode2D.Impulse);
			Debug.Log("눌림");
		}
		RaycastHit2D rayHit = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f,Vector2.down,0.02f,LayerMask.GetMask("Ground"));
		if(rayHit.collider != null)
		{
			anim.SetBool("isJump", false);
	
			isJump = false;
		}
		else
		{
			anim.SetBool("isJump", true);
			anim.SetBool("isMove", false);
			isJump = true;
		}
	}
}
