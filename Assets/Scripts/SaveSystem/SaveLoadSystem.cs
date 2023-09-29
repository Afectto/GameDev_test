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

	[SerializeField] private GameObject enemyPrefab;
	private static GameObject staticEnemyPrefab;

	private static SpawnEnemy spawn;

	[SerializeField] private List<GameObject> pickupList;
	private static List<GameObject> pickupStaticList;


	[SerializeField] private List<GameObject> slotItemList;
	private static List<GameObject> slotItemStaticList;

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
		pickupStaticList = pickupList;
		slotItemStaticList = slotItemList;
		LoadGame();

		return true;
	}

	public static void SaveGame()
	{
		BinaryFormatter formatter = new BinaryFormatter();
		FileStream stream = new FileStream(filePath, FileMode.Create);

		updateEnemyList();

		SaveData data = new();
		data.SaveEnemies(EnemyList);
		data.SavePickup(GameObject.FindObjectsOfType<Pickup>().ToList<Pickup>());
		data.SaveInventory(GameObject.FindObjectOfType<Inventory>());

		formatter.Serialize(stream, data);
		stream.Close();
	}

	public static void LoadGame()
	{
		if (!File.Exists(filePath))
			return;

		BinaryFormatter formatter = new BinaryFormatter();
		FileStream stream = new FileStream(filePath, FileMode.Open);

		SaveData data = formatter.Deserialize(stream) as SaveData;
		stream.Close();

		LoadEnemy(data.EnamiesData);
		LoadPickup(data.PicksUpData);
		LoadInventory(data.InventoriesData);
	}


	public static void LoadEnemy(List<EnemyData> data)
	{

		updateEnemyList();

		var player = LoadPlayer(data.Find(player => player.type == typeof(PlayerController).ToString()));

		foreach (var enemy in data)
		{
			var typeIEnemy = enemy.type;
			var findEnemy = EnemyList.Find(e => e.InstanceID == enemy.instanceID);
 			if (findEnemy != null)
			{
				if (typeIEnemy == typeof(Enemy).ToString())
				{
					var myEnemy = findEnemy as Enemy;
					myEnemy.target = null;
				}
				findEnemy.LoadEnemy(enemy);
			}
			else
			{
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

	public static void LoadPickup(List<PickUpData> data)
	{
		foreach (var pickup in data)
		{
			Instantiate(pickupStaticList.Find(up => up.tag == pickup.Tag), new Vector3(pickup.Position.x, pickup.Position.y, pickup.Position.z), Quaternion.identity);
		}
	}

	public static void LoadInventory(List<InventoryData> data)
	{
		var inventory = GameObject.FindObjectOfType<Inventory>();
		foreach (var slot in data)
		{
			for (int i = 0; i < slot.count; i++)
			{
				Instantiate(slotItemStaticList.Find(prefab => prefab.tag == slot.Tag), inventory.Slots[slot.index].transform);
			}
		}
	}

	void OnApplicationQuit()
	{
		SaveGame();
	}


	public void onLoad()
	{
		var allEnemy = GameObject.FindObjectsOfType<Enemy>().ToList<Enemy>();
		foreach (var enemy in allEnemy)
		{
			enemy.DestroyWhitDrop(false);
		}

		var inventory = GameObject.FindObjectOfType<Inventory>();
		foreach (var slots in inventory.Slots)
		{
			foreach (Transform child in slots.transform)
			{
				if (child.tag != "Text")
				{
					GameObject.Destroy(child.gameObject);
				}
			}

		}

		var allPickup =  GameObject.FindObjectsOfType<Pickup>().ToList<Pickup>();
		foreach (var pickup in allPickup)
		{
			Destroy(pickup.gameObject);
		}
	}
}