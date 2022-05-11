using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
	private Rigidbody2D rigi;
	[SerializeField] private float speed = 10;
	private void Start()
	{
		rigi = GetComponent<Rigidbody2D>();
	}
	private void Update()
	{
		transform.Translate(new Vector2.)
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Enemy"))
		{
			//적 데미지
		}
	}
}
