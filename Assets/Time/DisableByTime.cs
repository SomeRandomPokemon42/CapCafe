using Unity.VisualScripting;
using UnityEngine;

public class DisableByTime : MonoBehaviour
{
	public float DisableRadius = 5f;
	public int TimeToDisable = 0;
	public int TimeToEnable = 0;
	private bool ShouldIBeEnabled = true;
	private bool AmIEnabled = true;
	private TimeScript GameTime = null;
	private SpriteRenderer sprite = null;
	private Interactable interact = null;
	// Start is called before the first frame update
	void Start()
	{
		interact = GetComponent<Interactable>();
		sprite = GetComponent<SpriteRenderer>();
		GameTime = GameObject.FindGameObjectWithTag("GameController").GetComponent<TimeScript>();
		GameTime.HourHasPassed.AddListener(CheckTime);
		CheckTime();
	}
	private void CheckTime()
	{
		if (GameTime.hour == TimeToDisable)
		{ ShouldIBeEnabled = false; }
		if (GameTime.hour == TimeToEnable)
		{ ShouldIBeEnabled = true; }
		CanIDisable();
	}
	private void CanIDisable()
	{
		Collider[] collisions = new Collider[1]; // only 1 player
		Physics.OverlapSphereNonAlloc(transform.position, DisableRadius, collisions, 7, QueryTriggerInteraction.Ignore);
		if (collisions.Length == 0)
		{
			if (ShouldIBeEnabled)
			{
				AmIEnabled = true;
				sprite.enabled = true;
				interact.Disabled = false;
			} else
			{
				AmIEnabled = false;
				sprite.enabled = false;
				interact.Disabled = true;
			}
		}
		else if (ShouldIBeEnabled != AmIEnabled)
		{
			Invoke(nameof(CanIDisable), 3);
		}
	}
}
