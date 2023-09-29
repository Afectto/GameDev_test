using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
	[SerializeField] private SaveLoadSystem SaveLoadSystem;
	[SerializeField] private SpawnEnemy SpawnEnemy;

	private void Start()
	{
		if(!SaveLoadSystem.Initialize())
		{
			SpawnEnemy.Initialize();
		}
	}
}
