using UnityEngine;

public class SpriteAnimations : MonoBehaviour
{
	public Sprite UpSprite;
	public Sprite DownSprite;
	public Sprite LeftSprite;
	public Sprite RightSprite;
	private Animator MyAnimator;
	private SpriteRenderer MyRenderer;
	public Vector2 MovementVector = Vector2.zero;
	public enum SpriteDirection
	{ // These are the order of the sprites from top to bottom, and what the animator wants.
	  // Yes i know there's no 0, imagine being a flawwed puny human like me
		Down = 1,
		Left = 2,
		Right = 3,
		Up = 4
	}

	private void Start()
	{
		MyAnimator = GetComponent<Animator>();
		MyRenderer = GetComponent<SpriteRenderer>();
	}

	void Update()
	{
		if (Mathf.Abs(MovementVector.x) + Mathf.Abs(MovementVector.y) > 0.1f)
		{ // Animate based on direction
			MyAnimator.speed = 1f;
			if (Mathf.Abs(MovementVector.x) > Mathf.Abs(MovementVector.y))
			{ // Left or right
				if (MovementVector.x < 0)
				{
					MyAnimator.SetInteger("Direction", (int)SpriteDirection.Left);
				}
				else
				{
					MyAnimator.SetInteger("Direction", (int)SpriteDirection.Right);
				}
			}
			else
			{ // Up or down
				if (MovementVector.y < 0)
				{
					MyAnimator.SetInteger("Direction", (int)SpriteDirection.Down);
				}
				else
				{
					MyAnimator.SetInteger("Direction", (int)SpriteDirection.Up);
				}
			}
		}
		else
		{ // Don't animate, instead return to standing sprite
			int direction = MyAnimator.GetInteger("Direction");
			if (
				(MyRenderer.sprite != DownSprite && direction == (int)SpriteDirection.Down) ||
				(MyRenderer.sprite != UpSprite && direction == (int)SpriteDirection.Up) ||
				(MyRenderer.sprite != LeftSprite && direction == (int)SpriteDirection.Left) ||
				(MyRenderer.sprite != RightSprite && direction == (int)SpriteDirection.Right)
				)
			{
				MyAnimator.speed = 0.25f;
			}
			else
			{
				MyAnimator.speed = 0f;
			}
		}
	}
}
