using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
	public float speed;
	public Transform target;
	Vector3 lastEnemyPosition;
	// Start is called before the first frame update
	void Start()
	{
		lastEnemyPosition = Vector3.zero;
	}

	// Update is called once per frame
	void Update()
	{
		if (!target)
		{
			transform.position = Vector3.MoveTowards(transform.position, lastEnemyPosition, Time.deltaTime * speed);
			if (transform.position == lastEnemyPosition)
			{
				Destroy(gameObject);
			}
			return;
		}
		transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * speed);
		lastEnemyPosition = target.position;


		Vector3 diference = target.transform.position - transform.position;
		float rotationZ = Mathf.Atan2(diference.y, diference.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Enemy"))
		{
			target.GetComponentInParent<Enemy>().TakeDamage(Gun.dmg);
			Destroy(gameObject);
		}
	}
}
