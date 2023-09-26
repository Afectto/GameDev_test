
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class BaseEnemy: MonoBehaviour
{
	[Range(0, 10)] public float speed = 4f;

	public float Health = 100;
	[SerializeField, Range(1, 10000)] private float maxHealth = 100;

	[SerializeField] private Image HealthBar;

	private void Awake()
	{
		Health = maxHealth;
		HealthBar.fillAmount = 1;
	}
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
		if (damage < 0 && Health >= maxHealth)
		{
			Health = maxHealth;
		}
		else if (Health <= 0 && damage > 0)
		{
			Health = 0;
		}
		else
		{
			Health -= damage;
		}

		if (HealthBar) HealthBar.fillAmount = Health/maxHealth;
	}
}

