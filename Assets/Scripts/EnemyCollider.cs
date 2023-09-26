using System.Collections;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
	private bool isHit = false;
	private bool isPlayerInEnemyCollider = false;

	[SerializeField] private Enemy enemy;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			isPlayerInEnemyCollider = true;
			enemy.isNeedMove = false;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			isPlayerInEnemyCollider = false;
			enemy.isNeedMove = true;
		}
	}

	private void FixedUpdate()
	{
		if (!isHit && isPlayerInEnemyCollider)
		{
			StartCoroutine(hit());
		}
	}


	IEnumerator hit()
	{
		isHit = true;
		yield return new WaitForSeconds(enemy.hitSpeed);
		if (enemy.target)
		{
			enemy.target.GetComponent<PlayerController>().TakeDamage(enemy.damage);
		}
		isHit = false;
	}
}
