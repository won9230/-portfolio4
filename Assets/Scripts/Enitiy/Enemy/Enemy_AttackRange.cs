using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Enemy_AttackRange : MonoBehaviour
{
	[SerializeField] EnemyController enemy;
	bool CheckAttacking = false;

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.CompareTag("Player")) //공격 및 추적
		{
			if (!enemy.isHit)
			{
				enemy.StopMove();
				Vector2 playerPos = collision.transform.position;
				enemy.isAttack = true;
				if (playerPos.x > transform.position.x && !CheckAttacking)
				{
					//Debug.Log("Right");
					StartCoroutine(Enemy_Right_Move(2.5f));
				}
				else if (playerPos.x < transform.position.x && !CheckAttacking)
				{
					//Debug.Log("Left");
					StartCoroutine(Enemy_Left_Move(2.5f));
				}
			}
		}
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			enemy.isAttack = false;
			StopAllCoroutines();
			enemy.StartMove();
		}	
	}
	private IEnumerator Enemy_Right_Move(float coolTime)
	{
		CheckAttacking = true;
		enemy.anim.SetBool("Move", true);
		yield return new WaitForSeconds(0.2f);
		Vector2 vec = new Vector2(enemy.transform.position.x + 3.5f, enemy.transform.position.y);
		enemy.transform.DOLocalJump(vec, 0.5f, 1, 0.5f);
		enemy.spriteRenderer.flipX = false;
		yield return new WaitForSeconds(coolTime);
		CheckAttacking = false;
	}
	private IEnumerator Enemy_Left_Move(float coolTime)
	{
		CheckAttacking = true;
		enemy.anim.SetBool("Move", true);
		yield return new WaitForSeconds(0.2f);
		Vector2 vec = new Vector2(enemy.transform.position.x - 3.5f, enemy.transform.position.y);
		enemy.transform.DOLocalJump(vec, 0.5f, 1, 0.5f);
		enemy.spriteRenderer.flipX = true;
		yield return new WaitForSeconds(coolTime);
		CheckAttacking = false;
	}

}
