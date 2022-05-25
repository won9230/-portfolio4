using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : LivingEntity
{
	[SerializeField]private float moveSpeed = 5f;
	[SerializeField]private float jumpForce = 10f;
	private float jumpTimeLimlt = 0.2f;
	private float jumpTime = 0;

	[SerializeField] private Transform bulletPos;
	private Animator anim;
	private Rigidbody2D rigi;
	private BoxCollider2D coll;

	private int bulletCount = 0;
	private int jumpCount = 2;
	private bool isGround = true;
	private bool isJump = false;
	private bool isDoubleJump = false;


	public ObjectPoolingManager objectPooling;

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
		PlayerAttack();
		GroundChenk();
	}
	private void MovePlayer() //플레이어 이동
	{
		float h = Input.GetAxisRaw("Horizontal");
		rigi.AddForce(Vector2.right * h, ForceMode2D.Impulse);
		if (rigi.velocity.x > 1)
		{
			rigi.velocity = new Vector2(moveSpeed, rigi.velocity.y);
			this.transform.rotation = new Quaternion(0, 0, 0,0);
		}
		else if (rigi.velocity.x < -1)
		{
			rigi.velocity = new Vector2(-moveSpeed, rigi.velocity.y);
			this.transform.rotation = new Quaternion(0, 180, 0, 0);
		}
		if(h == 0)
		{
			rigi.velocity = new Vector2(rigi.velocity.normalized.x * 0.1f, rigi.velocity.y);
		}
		MoveAnim();
		if (Input.GetButtonUp("Horizontal"))
		{
			rigi.velocity = new Vector2(rigi.velocity.normalized.x * 0.5f, rigi.velocity.y);
		}
	}
	private void MoveAnim()//플레이어 이동 애니메이션
	{
		if (Input.GetAxisRaw("Horizontal") == 0 && isGround)
		{
			anim.SetBool("isMove",false);
		}
		else
		{
			anim.SetBool("isMove", true);
		}
	}
	private void Jump()//플레이어 점프
	{
		if (Input.GetKeyDown(KeyCode.LeftShift) && isGround)
		{
			Playerjump();
			isJump = true;
		}
		if(Input.GetKeyDown(KeyCode.LeftShift) && !isDoubleJump)
		{
			Playerjump();
			isDoubleJump = true;
		}

		if (isGround)
		{
			anim.SetBool("isJump", false);
		}
		else
		{
			anim.SetBool("isJump", true);
			anim.SetBool("isMove", false);
		}
	}
	private void Playerjump()
	{
		rigi.velocity = new Vector2(rigi.velocity.x, 0f);
		rigi.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
	}
	private void GroundChenk()
	{
		RaycastHit2D rayHit = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.02f, LayerMask.GetMask("Ground"));
		if (rayHit.collider != null)
		{
			isGround = true;
			isJump = false;
			isDoubleJump = false;
		}
		else
		{
			isGround = false;
		}
	}

	private void PlayerAttack()
	{
		if (Input.GetKeyDown(KeyCode.Z) && bulletCount <= 5)
		{
			if (transform.rotation.y == 0)
			{
				GameObject t_object = objectPooling.GetQueue();
				bulletCount++;
				t_object.transform.position = bulletPos.position;
				t_object.transform.rotation = new Quaternion(0, 0, 0, 0);
				StartCoroutine(bulletEnqueue(t_object));
			}
			else
			{
				GameObject t_object = objectPooling.GetQueue();
				bulletCount++;
				t_object.transform.position = bulletPos.position;
				t_object.transform.rotation = new Quaternion(0,180,0,0);
				StartCoroutine(bulletEnqueue(t_object));
			}
		}
	}
	private IEnumerator bulletEnqueue(GameObject _t_object)
	{
		yield return new WaitForSeconds(1.5f);
		objectPooling.InsertQueue(_t_object);
		bulletCount--;
	}
}
