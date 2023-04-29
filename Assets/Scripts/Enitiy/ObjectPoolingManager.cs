using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingManager : MonoBehaviour
{
	//public GameObject OPprefab = null;
	public GameObject poolingObject;
	public GameObject parent;
	[SerializeField] private int ea = 5;

	public Queue<GameObject> m_queue = new Queue<GameObject>();
	private void Start()
	{
		for (int i = 0; i < ea; i++)
		{
			GameObject t_object = Instantiate(poolingObject, Vector3.zero, Quaternion.identity);
			if (poolingObject == null)
			{
				Debug.Log("오브젝트가 없습니다");
			}
			else
			{
				t_object.transform.parent = parent.transform;
			}
			m_queue.Enqueue(t_object);
			t_object.SetActive(false);
		}
	}
	public void InsertQueue(GameObject p_object)
	{
		m_queue.Enqueue(p_object);
		p_object.SetActive(false);
	}
	public GameObject GetQueue()
	{
		GameObject t_object = m_queue.Dequeue();
		t_object.SetActive(true);
		return t_object;
	}
}
