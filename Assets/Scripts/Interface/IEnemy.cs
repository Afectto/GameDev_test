using UnityEngine;

public interface IEnemy
{
	float Speed { get; set; }

	Vector3 Position { get; set; }

	float Health { get; set; }

	float MaxHealth { get; set; }

	string InstanceID { get;}

	string Type { get; }

	public void Move(Vector3 pos);

	public void TakeDamage(float damage);


	public void LoadEnemy(SaveData.EnemyData enemy);
}