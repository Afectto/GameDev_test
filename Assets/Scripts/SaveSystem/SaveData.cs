using System.Collections.Generic;
using UnityEngine.UIElements;

[System.Serializable]
public class SaveData
{
	[System.Serializable]
	public struct Vec3
	{
		public float x, y, z;
		public Vec3(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}
	}

	[System.Serializable]
	public struct EnemyData
	{
		public Vec3 position;
		public float speed;

		public float health;
		public float maxHealth;

		public string instanceID;
		public string type;

		public EnemyData(Vec3 position, float speed, float health, float maxHealth, string InstanceID, string type)
		{
			this.position = position;
			this.speed = speed;
			this.health = health;
			this.maxHealth = maxHealth;
			this.instanceID = InstanceID;
			this.type = type;
		}
	}
	
	public List<EnemyData> EnamiesData = new();

	public void SaveEnemies(List<IEnemy> enemies)
	{
		foreach (var item in enemies)
		{
			Vec3 pos = new Vec3(item.Position.x, item.Position.y, item.Position.z);

			EnamiesData.Add(new EnemyData(pos, item.Speed, item.Health, item.MaxHealth, item.InstanceID, item.Type));
		}
	}


	[System.Serializable]
	public struct InventoryData
	{
		public int count;
		public int index;
		public string Tag;

		public InventoryData(string Tag, int index, int count)
		{
			this.Tag = Tag;
			this.count = count;
			this.index = index;
		}
	}

	public List<InventoryData> InventoriesData = new();

	public void	SaveInventory(Inventory inventory) 
	{
		Slot Slot;
		foreach (var item in inventory.Slots)
		{
			Slot = item.GetComponent<Slot>();
			InventoriesData.Add(new InventoryData(Slot.typeItem, Slot.getIndex(), Slot.transform.childCount-1));
		}
	}

	[System.Serializable]
	public struct PickUpData
	{
		public Vec3 Position;
		public string Tag;

		public PickUpData(Vec3 position, string Tag)
		{
			this.Position = position;
			this.Tag = Tag;
		}
	}

	public List<PickUpData> PicksUpData = new();

	public void SavePickup(List<Pickup> pickup)
	{
		foreach (var item in pickup)
		{
			Vec3 pos = new Vec3(item.transform.position.x, item.transform.position.y, item.transform.position.z);

			PicksUpData.Add(new PickUpData(pos, item.tag));
		}
	}

}
