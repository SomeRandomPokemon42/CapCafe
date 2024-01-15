using UnityEngine;
using UnityEngine.AI;

public class SpriteAnimations : MonoBehaviour
{
	[SerializeField] [Tooltip("The maximum speed we can move without animating")] private float StopThreshold = 0.1f;
	public Sprite UpSprite;
	public Sprite DownSprite;
	public Sprite LeftSprite;
	public Sprite RightSprite;
	private Animator MyAnimator;
	private SpriteRenderer MyRenderer;
	public Vector2 MovementVector = Vector2.zero;
	private bool DoItYourselfMode = false;
	private NavMeshAgent Agent = null;
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
		if (gameObject.GetComponentInParent<NavMeshAgent>() != null)
		{
			DoItYourselfMode = true;
			Agent = gameObject.GetComponentInParent<NavMeshAgent>();
		}
	}

	void Update()
	{
		// Nothing will be writing to MovementVector, so we have to
		if (DoItYourselfMode)
		{
			if (Mathf.Abs(Agent.velocity.x) + Mathf.Abs(Agent.velocity.y) + Mathf.Abs(Agent.velocity.z) < StopThreshold)
			{
				MovementVector = Vector2.zero;
			} else
			{
				MovementVector.x = transform.parent.forward.normalized.x;
				MovementVector.y = transform.parent.forward.normalized.z;
			}
		}


		if (Mathf.Abs(MovementVector.x) + Mathf.Abs(MovementVector.y) > StopThreshold)
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