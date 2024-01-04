using UnityEngine;

public class StockMarketScript : MonoBehaviour
{
	private TimeScript GameTime;
	private void Start()
	{
		GameTime = GetComponent<TimeScript>();
	}
	public int STONKS(GameItem Item)
	{
		int Modifier = GameTime.day + SystemInfo.systemMemorySize + Item.Name.Length;
		int NewValue = Mathf.RoundToInt(Mathf.PerlinNoise1D(Modifier) * Item.BaseValue + Item.BaseValue);
		if (NewValue <= 0)
		{
			NewValue += (0 - NewValue) + 1;
		}
		return NewValue;
	}
}
