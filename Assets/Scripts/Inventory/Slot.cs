using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class Slot : MonoBehaviour
{
	public GameObject canvas;
	public Button deleteButton;
	private Button my_Button;
	private Inventory inventory;
	private int index = -1;

	public string typeItem;

	public Text counter;

	private void Start()
	{
		canvas = GameObject.FindGameObjectWithTag("Inventory");
		inventory = GameObject.FindGameObjectWithTag("Player").GetComponentInParent<Inventory>();
	}

	private void Update()
	{
		if (!RectTransformUtility.RectangleContainsScreenPoint(GetComponent<RectTransform>(), Input.mousePosition, null)
			&& Input.GetMouseButtonDown(0) && my_Button)
		{
			Destroy(my_Button.gameObject);
			my_Button = null;
		}

		if(transform.childCount <= 1 && index >= 0)
		{
			inventory.isFill[index] = false;
		}

		var isTextShow = transform.childCount - 1 > 1;
		counter.gameObject.SetActive(isTextShow);
		counter.text = (transform.childCount - 1).ToString();
	}

	public void deleteItemButton()
	{
		if(!my_Button)
		{
			my_Button = GameObject.Instantiate(deleteButton, Vector3.zero, Quaternion.identity) as Button;
			my_Button.transform.SetParent(canvas.transform);

			my_Button.onClick.AddListener(this.deliteChild);

			my_Button.transform.position = Input.mousePosition;
		}
	}

	public void deliteChild()
	{
		foreach (Transform child in transform)
		{
			if(child.tag != "Text")
			{
				GameObject.Destroy(child.gameObject);
			}
		}
		Destroy(my_Button.gameObject);
		my_Button = null;
		typeItem = "";
	}

	public void setIndex(int m_index)
	{
		index = m_index;
	}
	public int getIndex()
	{
		return index;
	}
}
