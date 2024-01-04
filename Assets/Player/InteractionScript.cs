using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionScript : MonoBehaviour
{
	[SerializeField] private float InteractRadius = 2f;
	public void AttemptInteract(InputAction.CallbackContext callbackContext)
	{
		if (callbackContext.started)
		{
            Collider[] Potential = Physics.OverlapSphere(transform.position, InteractRadius);
            Interactable Target = null;
            float TargetDistance = InteractRadius * 50;
            foreach (Collider collider in Potential)
            {
                if (collider.GetComponent<Interactable>() != null || collider.GetComponent<InventoryInteractable>())
                {
                    // Is it usable?
                    bool Active = false;
					if (collider.GetComponent<Interactable>())
					{
						Active = !collider.GetComponent<Interactable>().Disabled;
					}
					if (collider.GetComponent<InventoryInteractable>())
					{
						Active = !collider.GetComponent<InventoryInteractable>().Disabled;
					}

					// Acting as though the Y is 0 equalizes things, making the math cleaner.
					float Distance = Vector3.Distance(
                        new Vector3(transform.position.x, 0f, transform.position.z),
                        new Vector3(collider.transform.position.x, 0f, collider.transform.position.z));

                    if (TargetDistance > Distance && Active)
                    {
                        TargetDistance = Distance;
                        collider.TryGetComponent(out Target);
                        if (Target == null)
                        {
                            Target = collider.GetComponent<InventoryInteractable>();
                        }
                    }
                }
            }
            if (Target != null)
            {
                Target.Interact();
            }
        }
	}
}
