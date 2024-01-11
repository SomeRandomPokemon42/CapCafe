using System;
using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
	public int amount;

	// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	// STATIC FUNCTIONS
	// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	// These functions help program things, and dont modify anything on their own
	public static Tuple<int, int, int, int> CoinConversion(int price)
	{
		// Get money
		int copper = price;
		int silver = Mathf.FloorToInt(copper / 10);
		copper -= silver * 10;
		int gold = Mathf.FloorToInt(silver / 10);
		silver -= gold * 10;
		int platinum = Mathf.FloorToInt(gold / 10);
		gold -= platinum * 10;
		// Convert down
		for (int p = platinum; p >= 100; p--)
		{
			platinum--;
			gold += 10;
		}
		for (int g = gold; g >= 100; g--)
		{
			gold--;
			silver += 10;
		}
		for (int s = silver; s >= 100; s--)
		{
			silver--;
			copper += 10;
		}
		for (int c = copper; c >= 100; c--)
		{
			copper--;
		}
		return new Tuple<int, int, int, int>(copper, silver, gold, platinum);
	}
}
