using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : BaseEnemy
{
	private bool isGamePlay = true;
	[Min(0)]public float damage = 3;
	public float hitSpeed = 0.5f;
	public bool isNeedMove;

	public Transform target;

	public GameObject[] dropList;

	private void FixedUpdate()
	{
		if (target)
		{
			Move(Vector3.one);
		}
	}

	public override void Move(Vector3 pos)
	{
		if(isNeedMove)
		{
			transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime);
		}
	}

	void OnApplicationQuit()
	{
		isGamePlay = false;
	}

	private void OnDestroy()
	{
		if (isGamePlay)
		{
			int itemIndex = Random.Range(0, dropList.Length);

			Instantiate(dropList[itemIndex], transform.position, Quaternion.identity);
		}
	}

	public void setTarget(Transform targ)
	{
		target = targ;
	}

}
