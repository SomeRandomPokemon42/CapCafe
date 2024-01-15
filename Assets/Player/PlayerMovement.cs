using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
	public Vector2 inputtedVector = Vector2.zero;
	public float speed = 5f;
	private SpriteAnimations AnimationFix;
	public bool MovementLocked = false;

	private void Awake()
	{
		AnimationFix = GetComponentInChildren<SpriteAnimations>();
	}
	public void WASD(InputAction.CallbackContext context)
	{
		if (!MovementLocked)
		{
			//Movement
			inputtedVector = context.ReadValue<Vector2>();
			//Animation
			if (AnimationFix != null)
			{
				AnimationFix.MovementVector = inputtedVector;
			}
		}
		else
		{
			inputtedVector = new Vector2(0, 0);
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (MovementLocked)
		{
			inputtedVector = new Vector2(0, 0);
		}
		else
		{
			transform.Translate(new Vector3(inputtedVector.x, 0, inputtedVector.y) * Time.deltaTime * speed);
		}
	}
}
