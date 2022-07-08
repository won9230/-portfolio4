using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : LivingEntity
{
	Rigidbody2D rigi;
	Animator anim;
	SpriteRenderer spriteRenderer;

	public int nextMove;
	private void Start()
	{
		rigi = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		Invoke("ThinkEnemy",5);
	}
	private void Update()
	{
		rigi.velocity = new Vector2(nextMove, rigi.velocity.y);
		EnemyRay();
	}
	private void EnemyRay()
	{
		Vector2 frontVec = new Vector2(rigi.position.x + nextMove*0.5f, rigi.position.y);

		Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
		RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1,LayerMask.GetMask("Ground"));
		if(rayHit.collider == null)
		{
			nextMove *= -1;
			CancelInvoke();
			Invoke("ThinkEnemy", 5);
		}
	}
	private void ThinkEnemy()
	{
		nextMove = Random.Range(-1, 2);


		anim.SetInteger("WalkSpeed",nextMove);

		if (nextMove != 1) 
			spriteRenderer.flipX = true;

		float nextTime = Random.Range(2f, 5f);
		Invoke("ThinkEnemy", nextTime);
	}
}
