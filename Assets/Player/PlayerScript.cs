using UnityEngine.InputSystem;
using UnityEngine;
using System;

public class PlayerScript : MonoBehaviour
{
	Vector2 inputtedVector = Vector2.zero;
	Animator MyAnimator;
	public float speed = 5f;

    private void Awake()
    {
        MyAnimator = GetComponent<Animator>();
    }
    public void WASD(InputAction.CallbackContext context)
	{
		//Movement
		inputtedVector = context.ReadValue<Vector2>();
		//Animation
		MyAnimator.speed = Mathf.Abs(inputtedVector.x) + Mathf.Abs(inputtedVector.y);
        if (Mathf.Abs(inputtedVector.x) + Mathf.Abs(inputtedVector.y) > 0.25f)
		{
			if (MathF.Abs(inputtedVector.x) > MathF.Abs(inputtedVector.y))
			{ //Left or Right
				if (inputtedVector.x > 0)
				{
					MyAnimator.SetInteger("Direction", 3); //Left
				}
				else
				{
					MyAnimator.SetInteger("Direction", 2); //Right
				}
			}
			else
			{ //Up or Down
				if (inputtedVector.y > 0)
				{
					MyAnimator.SetInteger("Direction", 4); //Up
				} else
				{
					MyAnimator.SetInteger("Direction", 1); // Down
				}
			}
		}
	}

	// Update is called once per frame
	void Update()
	{
		transform.Translate(inputtedVector * Time.deltaTime * speed);
	}
}
