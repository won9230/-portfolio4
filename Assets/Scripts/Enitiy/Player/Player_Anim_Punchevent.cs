using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Anim_Punchevent : MonoBehaviour
{
	[SerializeField] PlayerController playerController;
	[SerializeField] BoxCollider2D box;

	private void Start()
	{
		box.gameObject.SetActive(false);
	}

	public void IsAttack_Check_true() //애니매이션 이벤트 체크
	{
		playerController.isAttack = true;
		//Debug.Log(playerController.isAttack);
	}
	public void IsAttack_Check_false() //애니매이션 이벤트 체크
	{
		playerController.isAttack = false;
		//Debug.Log(playerController.isAttack);
	}
	public void IsAttack_CurAttackBox_true() //애니매이션 이벤트 체크
	{
		box.gameObject.SetActive(true);
	}
	public void IsAttack_CurAttackBox_false() //애니매이션 이벤트 체크
	{
		box.gameObject.SetActive(false);
	}
}
