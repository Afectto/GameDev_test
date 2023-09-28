using UnityEngine;


public class PlayerController : BaseEnemy
{
	public Joystick joystick;
	private Vector2 playerMovement = Vector2.zero;


	private void FixedUpdate()
	{
		playerMovement.x = joystick.Horizontal * speed;
		playerMovement.y = joystick.Vertical * speed;
		
		Move(transform.position);
	}

	public override void Move(Vector3 pos)
	{
		GetComponent<Rigidbody2D>().velocity = playerMovement;
	}

	private void OnDestroy()
	{
		if (isGamePlay)
		{
			SaveLoadSystem.RemoveEnemyDataByID(GetInstanceID().ToString());
		}
	}
}

