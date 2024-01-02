using UnityEngine;

public class DaylightScript : MonoBehaviour
{
	private Light Sun;
	private TimeScript GameTime;
	[SerializeField] private Gradient LightColor = new();

	// Start is called before the first frame update
	void Start()
	{
		Sun = gameObject.GetComponent<Light>();
		GameTime = gameObject.GetComponentInParent<TimeScript>();
	}

	private void Update()
	{
		float precent = GameTime.hour / 24f + (GameTime.minute / 60f) / 24f;
		if (precent > 1)
		{
			precent -= 1;
		}
		Color SunColor = LightColor.Evaluate(precent);
		Sun.color = SunColor;
	}
}
