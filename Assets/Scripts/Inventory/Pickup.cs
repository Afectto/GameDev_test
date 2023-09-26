using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Pickup : MonoBehaviour
{
	private Inventory inventory;
	public GameObject slotButton;

	private void Start()
	{
		inventory = GameObject.FindGameObjectWithTag("Player").GetComponentInParent<Inventory>();
		GetComponent<BoxCollider2D>().isTrigger = true;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			int emptySlot = addInStack();
			if (emptySlot >= 0)
			{
				addInNewSlot(emptySlot);
			}
		}
	}

	void addItem(int i){
		Instantiate(slotButton, inventory.Slots[i].transform);
		Destroy(gameObject);
	}

	int addInStack()
	{
		int saveFirstEmptySlot = -1;
		for (int i = 0; i < inventory.Slots.Length; i++)
		{
			if (inventory.Slots[i].GetComponent<Slot>().typeItem == slotButton.tag)
			{
				addItem(i);
				return -1;
			}

			if (!inventory.isFill[i] && saveFirstEmptySlot < 0)
			{
				saveFirstEmptySlot = i;
			}
		}
		return saveFirstEmptySlot;
	}

	void addInNewSlot(int i)
	{
		if (!inventory.isFill[i])
		{
			inventory.isFill[i] = true;
			inventory.Slots[i].GetComponent<Slot>().typeItem = slotButton.tag;
			addItem(i);
		}
	}

}
