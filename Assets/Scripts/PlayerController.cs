using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : BaseEnemy
{

	float dirX, dirY;
	public Joystick joystick;
	private Vector2 playerMovement;

	public void Initialize()
	{
		GetComponent<CapsuleCollider2D>().isTrigger = true;
	}

	private void FixedUpdate()
	{
		playerMovement = Vector2.zero;
		playerMovement.x = joystick.Horizontal * speed;
		playerMovement.y = joystick.Vertical * speed;
		
		Move(transform.position);
	}

	public override void Move(Vector3 pos)
	{

		GetComponent<Rigidbody2D>().velocity = playerMovement;

	}

}

