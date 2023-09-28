using System.Collections.Generic;

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
}
