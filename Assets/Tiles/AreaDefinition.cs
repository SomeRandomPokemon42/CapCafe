using UnityEngine;
using UnityEngine.Events;

public class AreaDefinition : MonoBehaviour
{
	[SerializeField] UnityEvent WhenAreaEntered = null;
	[SerializeField] UnityEvent WhenAreaLeft = null;
	public int TriggerCount = 0;
	private void Start()
	{
		TriggerCount = 0;
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			TriggerCount++;
			if (TriggerCount == 1)
			{
				WhenAreaEntered.Invoke();
			}
		}
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			TriggerCount--;
			if (TriggerCount == 0)
			{
				WhenAreaLeft.Invoke();
			}
			if (TriggerCount < 0)
			{
				TriggerCount = 0;
			}
		}
	}
}
