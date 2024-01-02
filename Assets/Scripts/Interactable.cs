using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
	public bool Disabled = false;
	public UnityEvent TriggerThis;
	virtual public void Interact()
	{
		TriggerThis.Invoke();
	}
}
