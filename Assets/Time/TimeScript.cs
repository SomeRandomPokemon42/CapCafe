using UnityEngine;
using UnityEngine.Events;

public class TimeScript : MonoBehaviour
{
	public int day = 0;
	public int hour = 0;
	public int minute = 0;
	public float second = 0;
	public float TimeSpeed = 15;

	public UnityEvent HourHasPassed = new();
	// Update is called once per frame
	void Update()
	{
		second += Time.deltaTime * TimeSpeed;
		if (second >= 60)
		{
			second -= 60;
			minute += 1;
		}
		if (minute >= 60)
		{
			minute -= 60;
			hour += 1;
			HourHasPassed.Invoke();
		}
		if (hour >= 24)
		{
			hour -= 24;
			day += 1;
		}
	}
}
