using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
	public static GameObject currentTarget;
	private  List<GameObject> allTarget = new List<GameObject>();


	void FixedUpdate()
	{
		if (!currentTarget && allTarget.Count > 0)
		{
			currentTarget = allTarget[allTarget.Count-1];
			allTarget.Remove(currentTarget);
			//gun.target = currentTarget.transform;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Enemy"))
		{
			allTarget.Add(collision.gameObject);
		}
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Enemy"))
		{
			allTarget.Remove(collision.gameObject);
			currentTarget = null;
		}
	}
}
