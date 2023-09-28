using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Linq;
using static SaveData;
using System;

public class SaveLoadSystem : MonoBehaviour
{
	private static string filePath;

	private static List<IEnemy> EnemyList = new();

	private static GameObject staticEnemyPrefab;
	[SerializeField] private  GameObject enemyPrefab;

	private static SpawnEnemy spawn;

	public static void UpdateEnemyData(IEnemy enemy)
	{
		// Найдем врага в списке и обновим его данные.
		var findEnemy = EnemyList.Find(e => e.InstanceID == enemy.InstanceID);
		if (findEnemy != null)
		{
			findEnemy = enemy;
		}
	}

	public bool Initialize()
	{
		filePath = Application.persistentDataPath + "/SaveGame.save";

		if (!File.Exists(filePath))
			return false;

		spawn = new SpawnEnemy();
		staticEnemyPrefab = enemyPrefab;
		LoadEnemy();

		return true;
	}

	public static void SaveEnemy()
	{
		BinaryFormatter formatter = new BinaryFormatter();
		FileStream stream = new FileStream(filePath, FileMode.Create);

		updateEnemyList();

		SaveData data = new();
		data.SaveEnemies(EnemyList);

		formatter.Serialize(stream, data);
		stream.Close();
	}

	public static void LoadEnemy()
	{
		if (!File.Exists(filePath))
			return;

		BinaryFormatter formatter = new BinaryFormatter();
		FileStream stream = new FileStream(filePath, FileMode.Open);

		SaveData data = formatter.Deserialize(stream) as SaveData;
		stream.Close();

		updateEnemyList();

		var player = LoadPlayer(data.EnamiesData.Find(player => player.type == typeof(PlayerController).ToString()));

		foreach (var enemy in data.EnamiesData)
		{
			var findEnemy = EnemyList.Find(e => e.InstanceID == enemy.instanceID);
 			if (findEnemy != null)
			{
				findEnemy.LoadEnemy(enemy);
			}
			else
			{
				var typeIEnemy = enemy.type;
				if (typeIEnemy == typeof(Enemy).ToString())
				{
					EnemyList.Add(spawn.SpawnNewEnemy(player, staticEnemyPrefab).GetComponent<IEnemy>());
					EnemyList.Last().LoadEnemy(enemy);
				}
			}
		}
	}

	public static PlayerController LoadPlayer(EnemyData loadedPlayer)
	{
		var player = GameObject.FindObjectOfType<PlayerController>();

		var findPlayerInEnemyList = EnemyList.Find(e => e.InstanceID == loadedPlayer.instanceID);
		if (findPlayerInEnemyList != null)
		{
			EnemyList.Add(player);
		}
		else if(player == null)
		{
			SpawnPlayer spawnPlayer = GameObject.FindObjectOfType<SpawnPlayer>();
			player = spawnPlayer.spawnPlayer();
			EnemyList.Add(player);
		}

		if (loadedPlayer.maxHealth > 0)
		{
			player.LoadEnemy(loadedPlayer);
		}
		return player;
	}

	static void updateEnemyList()
	{
		EnemyList = GameObject.FindObjectsOfType<BaseEnemy>().ToList<IEnemy>();
	}

	public static void RemoveEnemyDataByID(string ID)
	{
		EnemyList.Remove(EnemyList.Find(e => e.InstanceID == ID));
	}

	public static int getCountEnemyList()
	{
		return EnemyList.FindAll(e => e.Type == "Enemy").Count;
	}


	void OnApplicationQuit()
	{
		SaveEnemy();
	}
}