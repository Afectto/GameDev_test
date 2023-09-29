using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
	public GameObject bullet;
	public Transform shootElement;

	public static float dmg = 1;
	public float shootSpeed;

	private Transform CurrentTarget;

	private Quaternion originalGunRotation;
	private Vector3  originalGunScale;

	private void Start()
	{
		originalGunRotation = transform.rotation;
		originalGunScale = transform.localScale;
	}

	// Update is called once per frame
	void Update()
	{
		if (PlayerTrigger.currentTarget)
		{
			CurrentTarget = PlayerTrigger.currentTarget.transform;
			RotateGun();
		}
		else
		{
			transform.rotation = originalGunRotation;
			transform.localScale = originalGunScale;
		}
	}

	private void RotateGun()
	{
		Vector3 diference = CurrentTarget.transform.position - transform.position;
		float rotationZ = Mathf.Atan2(diference.y, diference.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);

		Vector3 LocalScale = transform.localScale;

		if (rotationZ > 90 || rotationZ < -90)
		{
			LocalScale.y = -originalGunScale.y;
		}
		else
		{
			LocalScale.y = originalGunScale.y;
		}
		
		transform.localScale = LocalScale;
	}

	public void shoot()
	{
		if(CurrentTarget)
		{
			var m_bullet = GameObject.Instantiate(bullet, shootElement.position, Quaternion.identity) as GameObject;
			m_bullet.GetComponent<BulletController>().target = CurrentTarget;
		}
	}
}
