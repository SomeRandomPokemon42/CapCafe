using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
	public UnityEvent TriggerThis;
	virtual public void Interact()
	{
		TriggerThis.Invoke();
	}
}
