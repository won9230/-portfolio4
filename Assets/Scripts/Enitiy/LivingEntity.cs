using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour
{
	public float hp = 50f;
	public float maxHp = 50f;
	public float mp = 50f;
	public float maxMp = 50f;
	public bool dead = false;

	public virtual void Hit(float damege)
	{
		hp -= damege;
	}
	public void DeadEntity()
	{
		if(hp <= 0)
		{
			dead = true;
		}
	}
}
