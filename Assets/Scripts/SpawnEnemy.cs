using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{

	[SerializeField] private GameObject enemyPrefab;
	private static GameObject _enemyPrefab;
	static float RandX;
	static float RandY;

	[SerializeField] private int spawnCount = 0;

	[SerializeField] private static Transform playerTransform;


	// Start is called before the first frame update
	public void Initialize()
	{
		if(spawnCount > SaveLoadSystem.getCountEnemyList())
			{
				for (int i = 0; i < spawnCount - SaveLoadSystem.getCountEnemyList(); i++)
				{
					playerTransform = GameObject.FindAnyObjectByType<PlayerController>().transform;
					_enemyPrefab = enemyPrefab;
					SpawnEnemyInRandPosition();
				}
			}
	}

	static GameObject SpawnEnemyInRandPosition()
	{
		RandX = Random.Range(-40f, -11f);
		RandY = Random.Range(6f, -7f);

		Vector2 whereToSpawn = new Vector2(RandX, RandY);

		while(Vector3.Distance(whereToSpawn, playerTransform.position) < 7)
		{
			RandX = Random.Range(-40f, -11f);
			RandY = Random.Range(6f, -7f);
			whereToSpawn = new Vector2(RandX, RandY);
		}

		return Instantiate(_enemyPrefab, whereToSpawn, Quaternion.identity);

	}

	public GameObject SpawnNewEnemy(PlayerController player, GameObject enemy)
	{
		playerTransform = player.transform;
		_enemyPrefab = enemy;
		return SpawnEnemyInRandPosition();
	}
}
