using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
	[SerializeField] public GameObject player;
	[SerializeField] public int countInventorySlot;

	[SerializeField] public GameObject inventory;

	public void Initialize()
	{
		if(GameObject.FindFirstObjectByType<PlayerController>() == null)
		{
			spawnPlayer();
		}
	}

	public PlayerController spawnPlayer()
	{
		//var myPlayer = player.GetComponent<PlayerController>();

		//myPlayer.joystick = GameObject.FindObjectOfType<Joystick>();

		//myPlayer.GetComponent<Inventory>().inventory = inventory;
		//myPlayer.GetComponent<Inventory>().canvas = inventory;
		//myPlayer.GetComponent<Inventory>().countInventorySlot = countInventorySlot;
		//myPlayer.GetComponent<Inventory>().Initialize();

		//myPlayer.transform.position = transform.position;
		//myPlayer.transform.rotation = transform.rotation;
		//myPlayer.transform.localScale = transform.localScale;

		//Instantiate(player, myPlayer.transform.position, Quaternion.identity);

		//return myPlayer;
		return null;
	}
}
