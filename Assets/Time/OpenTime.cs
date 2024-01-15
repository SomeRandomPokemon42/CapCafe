using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OpenTime : MonoBehaviour
{
	[Header("GUI")]
	[SerializeField] private Image[] GUIThings;
	[SerializeField] private Sprite OpenTexture = null;
	[SerializeField] private Sprite ClosedTexture = null;
	Toaster toast = null;
	TimeScript GameTime;
	[Header("Time")]
	public int OpenAt = 10;
	public int CloseAt = 20;
	[Header("Cafe")]
	public bool Open = false;
	[Header("Events")]
	public UnityEvent CafeOpened = new();
	public UnityEvent CafeClosed = new();

	private void Start()
	{
		toast = GetComponent<Toaster>();
		MessWithGUI(false);
		GameTime = GetComponent<TimeScript>();
		GameTime.HourHasPassed.AddListener(ListenForTime);
	}
	private void ListenForTime()
	{
		if (GameTime.hour == OpenAt - 1)
		{
			toast.Toast("The Cafe opens in 1 hour!");
		}
		if (GameTime.hour == OpenAt)
		{
			OpenCafe();
		}
		if (GameTime.hour == CloseAt)
		{
			CloseCafe();
		}
	}
	public void OpenCafe()
	{
		MessWithGUI(true);
		toast.Toast("The Cafe is now open!");
		Open = true;
		CafeOpened.Invoke();
	}
	public void CloseCafe()
	{
		MessWithGUI(false);
		toast.Toast("The Cafe is now closed!");
		Open = false;
		CafeClosed.Invoke();
	}

	public void MessWithGUI(bool Open)
	{
		foreach (Image thing in GUIThings)
		{
			if (Open)
			{
				thing.sprite = OpenTexture;
			} else 
			{ 
				thing.sprite = ClosedTexture;
			}
		}
	}
}