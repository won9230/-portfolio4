using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Anim_Slime_Eevnt : MonoBehaviour
{
	EnemyController enemyController;
	private void Start()
	{
		enemyController = GetComponent<EnemyController>();
	}

	public void Slime_Anim_Move_False()
	{
		enemyController.anim.SetBool("Move",false);
	}
}
