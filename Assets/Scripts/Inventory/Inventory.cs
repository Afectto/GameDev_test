using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
	public bool[] isFill;
	public GameObject[] Slots;
	public GameObject canvas;
	public GameObject inventory;

	private bool isInventoryOn;
	[SerializeField] private GameObject SlotPrefab;

	private void Start()
	{
		isInventoryOn = false;
		inventory.SetActive(false);

		for (int i = 0; i < Slots.Length; i++)
		{
			Slots[i] = GameObject.Instantiate(SlotPrefab, Vector3.zero, Quaternion.identity);
			Slots[i].transform.SetParent(canvas.transform);

			Vector3 offsetX = canvas.transform.position;
			offsetX.x -= i * 64;
			Slots[i].transform.position = offsetX;
			Slots[i].GetComponent<Slot>().setIndex(i);
		}
    }


	public void Bug()
	{
		if(isInventoryOn)
		{
			isInventoryOn = false;
			inventory.SetActive(false);
		}
		else
		{
			isInventoryOn = true;
			inventory.SetActive(true);
		}
	}
}
