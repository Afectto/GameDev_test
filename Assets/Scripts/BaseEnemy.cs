
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class BaseEnemy: MonoBehaviour, IEnemy
{
	[Range(0, 10)] public float speed = 4f;

	public float health = 100;
	[SerializeField, Range(1, 10000)] private float maxHealth = 100;

	[SerializeField] private Image HealthBar;
	public bool isGamePlay = true;

	public float Speed { get => speed; set => speed = value; }
	public float Health { get => health; set => health = value; }
	public float MaxHealth { get => maxHealth; set => maxHealth = value; }
	public Vector3 Position { get => transform.position; set => transform.position = value; }
	public string InstanceID { get => GetInstanceID().ToString(); }
	public string Type => GetType().ToString();

	void Update()
	{
		if (Health <= 0)
		{
			Destroy(gameObject);
		}
	}
	public abstract void Move(Vector3 pos);

	public void TakeDamage(float damage)
	{
		if (damage < 0 && Health >= MaxHealth)
		{
			Health = MaxHealth;
		}
		else if (Health <= 0 && damage > 0)
		{
			Health = 0;
		}
		else
		{	
			Health -= damage;
		}

		if (HealthBar) HealthBar.fillAmount = Health / MaxHealth;
	}

	void resetHealthLine()
	{
		HealthBar.fillAmount = Health / MaxHealth;
	}

	private void Start()
	{
		Health = MaxHealth;
		HealthBar.fillAmount = 1;
	}

	public void LoadEnemy(SaveData.EnemyData enemy)
	{
		Health = enemy.health;
		maxHealth = enemy.maxHealth;

		Vector3 position;
		position.x = enemy.position.x;
		position.y = enemy.position.y;
		position.z = enemy.position.z;

		Position = position;
		Speed = enemy.speed;

		resetHealthLine();
	}


	void OnApplicationQuit()
	{
		isGamePlay = false;
	}
}

