using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Anim_Punchevent : MonoBehaviour
{
	public PlayerController playerController;

	public void IsAttack_Check_true()
	{
		playerController.isAttack = true;
		Debug.Log(playerController.isAttack);
	}
	public void IsAttack_Check_false()
	{
		playerController.isAttack = false;
		Debug.Log(playerController.isAttack);
	}
}
