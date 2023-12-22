using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
	[SerializeField] UnityEvent TriggerThis;
	public void Interact()
	{
		TriggerThis.Invoke();
	}
}
