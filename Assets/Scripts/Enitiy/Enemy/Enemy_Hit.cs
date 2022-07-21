using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy_Hit : MonoBehaviour
{
	[SerializeField] private EnemyController enemy;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player_Attack") && !enemy.isHit)
		{
			Debug.Log("적이 공격에 맞음");
			enemy.Hit(1); //플레이어 공격력(임시)
			StartCoroutine(Hit_animCor());
			collision.gameObject.SetActive(false);
			Vector2 playerPos = collision.transform.position;
			enemy.isAttack = true;
			if (playerPos.x > enemy.gameObject.transform.position.x)
			{
				enemy.spriteRenderer.flipX = false;
				enemy.gameObject.transform.DOLocalMoveX(enemy.transform.position.x-1, 0.5f);
			}
			else if (playerPos.x < enemy.gameObject.transform.position.x)
			{
				enemy.spriteRenderer.flipX = true;
				enemy.gameObject.transform.DOLocalMoveX(enemy.transform.position.x+1, 0.5f);
			}
		}
	}
	private IEnumerator Hit_animCor()
	{
		enemy.anim.SetBool("Hit", true);
		enemy.isHit = true;
		yield return new WaitForSeconds(0.3f);
		enemy.anim.SetBool("Hit", false);
		enemy.isHit = false;
	}

}
