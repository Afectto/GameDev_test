using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnEnemy : MonoBehaviour
{

	[SerializeField] private GameObject enemyPrefab;
	float RandX;
	float RandY;
	Vector2 whereToSpawn;

	[SerializeField] private int spawnCount = 0;
	[SerializeField] private Transform playerTransform;
	// Start is called before the first frame update
	void Start()
	{
		for (int i = 0; i < spawnCount; i++)
		{
			SpawnEnemyInRandPosition();
		}
	}

	void SpawnEnemyInRandPosition()
	{
		RandX = Random.Range(-40f, -11f);
		RandY = Random.Range(6f, -7f);
		whereToSpawn = new Vector2(RandX, RandY);
		while(Vector3.Distance(whereToSpawn, playerTransform.position) < 7)
		{
			RandX = Random.Range(-40f, -11f);
			RandY = Random.Range(6f, -7f);
			whereToSpawn = new Vector2(RandX, RandY);
		}

		Instantiate(enemyPrefab, whereToSpawn, Quaternion.identity);
	}
}
