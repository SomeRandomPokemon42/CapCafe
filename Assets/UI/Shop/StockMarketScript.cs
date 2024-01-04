using UnityEngine;

public class StockMarketScript : MonoBehaviour
{
	private TimeScript GameTime;
	private void Start()
	{
		GameTime = GetComponent<TimeScript>();
	}
	public int STONKS(int Base)
	{
		int NewValue = Mathf.RoundToInt(Mathf.PerlinNoise1D(GameTime.day + SystemInfo.systemMemorySize) * Base + Base);
		if (NewValue <= 0)
		{
			NewValue += (0 - NewValue) + 1;
		}
		return NewValue;
	}
}
