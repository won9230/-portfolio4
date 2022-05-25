using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
	[SerializeField] private float speed = 10;

	private void Update()
	{
		if (transform.rotation.y == 0)
		{
			transform.Translate(transform.right * speed * Time.deltaTime);
		}
		else
		{
			transform.Translate(transform.right * speed * -1 * Time.deltaTime);
		}
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Enemy"))
		{
			//적 데미지
		}
	}
}
