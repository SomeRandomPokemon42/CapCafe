using UnityEngine;

public class UIRunaway : MonoBehaviour
{
	[SerializeField] private UIBoxHandler UI;
	[SerializeField] private PlayerMovement movement;
	private InventoryManager PlayerInventory;
	public float RunawayDistance = 2;
	private float RunProgress = 0;
	private bool scanning = false;

	private void Start()
	{
		PlayerInventory = GameObject.FindGameObjectWithTag("GameController").GetComponent<Directions>().PlayerInventory;
	}
	private void Update()
	{
		if (scanning)
		{
			RunProgress += (Mathf.Abs(movement.inputtedVector.x) + Mathf.Abs(movement.inputtedVector.y)) * Time.deltaTime * movement.speed;
		}

		if (UI.UIDisplayed)
		{
			if (!scanning)
			{
				scanning = true;
				RunProgress = 0;
			}
			else
			{
				if (RunProgress > RunawayDistance)
				{
					InventoryManager MyInventory = GetComponentInChildren<InventoryManager>();
					if (MyInventory != null && MyInventory.MyStorage != null)
					{
						MyInventory.MyStorage.OnClose();
					}
					UI.DisableUI();
					PlayerInventory.SecondInventory = null;
					scanning = false;
				}
			}
		}
	}
}
