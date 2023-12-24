using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIRunaway : MonoBehaviour
{
	[SerializeField] private UIBoxHandler UI;
	[SerializeField] private PlayerMovement movement;
	public float RunawayDistance = 2;
	private float RunProgress = 0;
	private bool scanning = false;
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
			} else
			{
				if (RunProgress > RunawayDistance)
				{
					UI.DisableUI();
					scanning = false;
				}
			}
		}
	}
}
