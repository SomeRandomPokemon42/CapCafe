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
                if (collider.GetComponent<Interactable>() != null)
                {
                    // Acting as though the Y is 0 equalizes things, making the math cleaner.
                    float Distance = Vector3.Distance(
                        new Vector3(transform.position.x, 0f, transform.position.z),
                        new Vector3(collider.transform.position.x, 0f, collider.transform.position.z));

                    if (TargetDistance > Distance)
                    {
                        TargetDistance = Distance;
                        Target = collider.GetComponent<Interactable>();
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
