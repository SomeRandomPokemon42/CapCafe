using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
	[SerializeField] UnityEvent TriggerThis;
	virtual public void Interact()
	{
		TriggerThis.Invoke();
	}
}
