using UnityEngine.InputSystem;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
	Vector2 inputtedVector = Vector2.zero;
	public float speed = 5f;
	private SpriteAnimations AnimationFix;

    private void Awake()
    {
		AnimationFix = GetComponentInChildren<SpriteAnimations>();
    }
    public void WASD(InputAction.CallbackContext context)
	{
		//Movement
		inputtedVector = context.ReadValue<Vector2>();
		
		if (AnimationFix != null)
		{
			AnimationFix.MovementVector = inputtedVector;
		}
	}

	// Update is called once per frame
	void Update()
	{
		transform.Translate(new Vector3(inputtedVector.x, 0, inputtedVector.y) * Time.deltaTime * speed);
	}
}
