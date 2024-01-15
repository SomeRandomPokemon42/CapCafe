using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Toaster : MonoBehaviour
{
	[SerializeField] private UIBoxHandler Popup;
	private TextMeshProUGUI PopupText;
	[SerializeField] private float ToastTime = 2f;
	private float ToastTimeRemaining = 0f;
	private Queue<string> MessageQueue = new();
	private enum Step
	{
		WaitingForUIToHide,
		WaitingForUIToShow,
		WaitingForTimer,
		NoMessages
	}
	private Step step;

	// Start is called before the first frame update
	void Start()
	{
		PopupText = Popup.GetComponentInChildren<TextMeshProUGUI>();
	}
	public void Toast(string Message)
	{
		MessageQueue.Enqueue(Message);
	}
	private void Update()
	{
		switch (step)
		{
			case Step.NoMessages:
				if (MessageQueue.Count > 0)
				{
					step = Step.WaitingForUIToShow;
					PopupText.text = MessageQueue.Dequeue();
					Popup.EnableUI();
				}
				break;
			case Step.WaitingForUIToShow:
				if (Popup.MoveProgress > 0.98f)
				{
					ToastTimeRemaining = ToastTime;
					step = Step.WaitingForTimer;
					Popup.GetComponent<AudioSource>().Play();
				}
				break;
			case Step.WaitingForTimer:
				ToastTimeRemaining -= Time.deltaTime;
				if (ToastTimeRemaining <= 0f)
				{
					Popup.DisableUI();
					step = Step.WaitingForUIToHide;
				}
				break;
			case Step.WaitingForUIToHide:
				if (Popup.MoveProgress > 0.98f)
				{
					step = Step.NoMessages;
				}
				break;
		}
	}
}
