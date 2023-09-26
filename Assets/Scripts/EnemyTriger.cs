using UnityEngine;

public class EnemyTriger : MonoBehaviour
{
	[SerializeField] private Enemy enemy;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			enemy.setTarget(collision.GetComponentInParent<PlayerController>().gameObject.transform);
		}
	}
}
