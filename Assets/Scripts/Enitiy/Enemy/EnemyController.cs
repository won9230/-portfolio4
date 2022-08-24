using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyController : LivingEntity
{
	[HideInInspector] public Animator anim;
	[HideInInspector] public SpriteRenderer spriteRenderer;
	[SerializeField] private Enemy_AttackRange enemy_AttackRange;
	[SerializeField] private Enemy_Hit enemy_Hit;

	[HideInInspector] public bool isAttack = false; //공격 확인
	[HideInInspector] public bool isHit = false; //히트 확인
	[SerializeField] float enemy_Attack_Cooltime = 3;

	private void Awake()
	{
		//rigi = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}
	private void OnEnable()
	{
		StartMove();
	}
	private void OnDisable()
	{
		StopCoroutine(Enemy_Move(enemy_Attack_Cooltime));
	}
	private void Update()
	{
		DeadEnemy();
	}
	public void DeadEnemy()
	{
		DeadEntity();
		if (dead)
		{
			enemy_AttackRange.gameObject.SetActive(false);
			enemy_Hit.gameObject.SetActive(false);
			StopAllCoroutines();
			anim.SetTrigger("Dead");
		} 
	}
	private IEnumerator Enemy_Move(float coolTime)
	{
		while (true)
		{
			if (!isAttack)
			{
				var ran = Random.Range(-1, 2);
				if (ran == 1)
				{
					anim.SetBool("Move", true);
					yield return new WaitForSeconds(0.2f);
					Vector2 vec = new Vector2(transform.position.x + ran * 2, transform.position.y);
					transform.DOLocalJump(vec, 0.5f, 1, 0.5f);
					spriteRenderer.flipX = false;
				}
				else if (ran == -1)
				{
					anim.SetBool("Move", true);
					yield return new WaitForSeconds(0.2f);
					Vector2 vec = new Vector2(transform.position.x + ran * 2, transform.position.y);
					transform.DOLocalJump(vec, 0.5f, 1, 0.5f);
					spriteRenderer.flipX = true;
				}
				//Debug.Log("Move");
			}
			yield return new WaitForSeconds(coolTime);
		}
	}
	IEnumerator DestoryEnemy()
	{
		yield return new WaitForSeconds(3f);
		Destroy(transform.gameObject);
	}
	public void StopMove() //플레이어 추적하기 전에 멈추기
	{
		StopCoroutine(Enemy_Move(0));
	}
	public void StartMove()
	{
		StartCoroutine(Enemy_Move(3f));
	}

}
