using System;
using TMPro;
using UnityEngine;

public class UICoinDisplayScript : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI CopperText;
	[SerializeField] private TextMeshProUGUI SilverText;
	[SerializeField] private TextMeshProUGUI GoldText;
	[SerializeField] private TextMeshProUGUI PlatinumText;
	[SerializeField] private PlayerMoney money;
	private int OldMoney = 0;

	private string ConvertNumberToString2(int number)
	{
		if (number > 9)
		{
			return number.ToString();
		}
		else return "0" + number.ToString();
	}

	// Update is called once per frame
	void Update()
	{
		if (money.amount != OldMoney)
		{
			OldMoney = money.amount;
			// Write to TextMesh
			Tuple<int, int, int, int> coins = PlayerMoney.CoinConversion(money.amount);
			CopperText.text = ConvertNumberToString2(coins.Item1);
			SilverText.text = ConvertNumberToString2(coins.Item2);
			GoldText.text = ConvertNumberToString2(coins.Item3);
			PlatinumText.text = ConvertNumberToString2(coins.Item4);
		}
	}
}
