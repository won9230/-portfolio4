using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerWeaponState
{
	Punch = 0,
	Sword = 1,
	Bow = 2,
}
public class PlayerController : LivingEntity
{
	[SerializeField]private float moveWalkSpeed = 5f;
	[SerializeField]private float moveRunSpeed = 8f;
	[SerializeField]private float jumpForce = 10f;

	//[SerializeField] private Transform bulletPos;
	private Animator[] anim;
	private Rigidbody2D rigi;
	private BoxCollider2D coll;

	private int jumpCount = 2;
	private bool isGround = true;
	[HideInInspector]public bool isAttack = false;

	//public ObjectPoolingManager objectPooling;

	private void Start()
	{
		anim = GetComponentsInChildren<Animator>();
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
		if (!isAttack)
		{
			float h = Input.GetAxisRaw("Horizontal");
			rigi.AddForce(Vector2.right * h, ForceMode2D.Impulse);
			if (rigi.velocity.x > 1)
			{
				rigi.velocity = new Vector2(moveWalkSpeed, rigi.velocity.y);
				this.transform.rotation = new Quaternion(0, 0, 0, 0);
			}
			else if (rigi.velocity.x < -1)
			{
				rigi.velocity = new Vector2(-moveWalkSpeed, rigi.velocity.y);
				this.transform.rotation = new Quaternion(0, 180, 0, 0);
			}
			if (h == 0)
			{
				rigi.velocity = new Vector2(rigi.velocity.normalized.x * 0.1f, rigi.velocity.y);
			}
			PlayerRun();
		}
			MoveAnim();
	}
	private void PlayerRun()
	{
		if (Input.GetKey(KeyCode.LeftShift))
		{
			if (rigi.velocity.x > 1)
			{
				rigi.velocity = new Vector2(moveRunSpeed, rigi.velocity.y);
				this.transform.rotation = new Quaternion(0, 0, 0, 0);
			}
			else if (rigi.velocity.x < -1)
			{
				rigi.velocity = new Vector2(-moveRunSpeed, rigi.velocity.y);
				this.transform.rotation = new Quaternion(0, 180, 0, 0);
			}
		}
	}
	private void MoveAnim()//플레이어 이동 애니메이션
	{
		if (Input.GetAxisRaw("Horizontal") == 0 || !isGround)
		{
			if (Input.GetKey(KeyCode.LeftShift))
			{
				anim[0].SetBool("isMove", false);
				anim[0].SetBool("isRun", true);
			}
			else
			{
				anim[0].SetBool("isRun", false);
				anim[0].SetBool("isMove", false);
			}
		}
		else
		{
			if (!Input.GetKey(KeyCode.LeftShift))
			{
				anim[0].SetBool("isMove", true);
				anim[0].SetBool("isRun", false);
			}
			else
			{
				anim[0].SetBool("isRun", true);
				anim[0].SetBool("isMove", false);
			}
		}
	}
	private void Jump()//플레이어 점프
	{
		if (Input.GetKeyDown(KeyCode.X) && jumpCount > 0)
		{
			Playerjump();
		}
		PlayerJumpAnim();
	}
	private void Playerjump()
	{
		rigi.velocity = new Vector2(rigi.velocity.x, 0f);
		rigi.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
		jumpCount--;

		StopCoroutine(PlayerJumpAnimCor());
		StartCoroutine(PlayerJumpAnimCor());
	}
	private void PlayerJumpAnim()
	{
		if (isGround)
		{
			anim[0].SetBool("isJumpDown", false);
		}
	}
	IEnumerator PlayerJumpAnimCor()
	{
		anim[0].SetBool("isMove", false);
		anim[0].SetBool("isJumpUp", true);
		yield return new WaitForSeconds(0.2f);
		anim[0].SetBool("isJumpUp", false);
		anim[0].SetBool("isJumpDown", true);
	}
	private void GroundChenk()
	{
		RaycastHit2D rayHit = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.01f, LayerMask.GetMask("Ground"));
		if (rayHit.collider != null)
		{
			isGround = true;
			jumpCount = 1;
		}
		else
		{
			isGround = false;
		}
	}
	private void PlayerAttack()
	{
		if (Input.GetKeyDown(KeyCode.Z))
		{
			anim[0].SetTrigger("Cur_Attack");
			//StopCoroutine(AttackCooltime(0.5f));	
			//StartCoroutine(AttackCooltime(0.5f));	
		}
	}
	private IEnumerator AttackCooltime(float cooltime)
	{
		isAttack = true;
		yield return new WaitForSeconds(cooltime);
		isAttack = false;
	}
}
