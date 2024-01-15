using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PriceTagScript : MonoBehaviour
{
	[SerializeField] Image PlatinumImage;
	[SerializeField] Image GoldImage;
	[SerializeField] Image SilverImage;
	[SerializeField] Image CopperImage;
	[SerializeField] TextMeshProUGUI PlatinumText;
	[SerializeField] TextMeshProUGUI GoldText;
	[SerializeField] TextMeshProUGUI SilverText;
	[SerializeField] TextMeshProUGUI CopperText;
	private LayoutElement PlatinumElement;
	private LayoutElement GoldElement;
	private LayoutElement SilverElement;
	private LayoutElement CopperElement;
	[Header("---------")]
	public int Price = 0;
	private int OldPrice = 100;

	private void Start()
	{
		PlatinumElement = PlatinumImage.GetComponentInParent<LayoutElement>();
		GoldElement = GoldImage.GetComponentInParent<LayoutElement>();
		SilverElement = SilverImage.GetComponentInParent<LayoutElement>();
		CopperElement = CopperImage.GetComponentInParent<LayoutElement>();
	}

	// Update is called once per frame
	void Update()
	{
		if (Price != OldPrice)
		{
			OldPrice = Price;
			Tuple<int, int, int, int> cost = PlayerMoney.CoinConversion(Price);
			CopperText.text = cost.Item1.ToString();
			SilverText.text = cost.Item2.ToString();
			GoldText.text = cost.Item3.ToString();
			PlatinumText.text = cost.Item4.ToString();
			if (cost.Item4 == 0)
			{
				PlatinumText.enabled = false;
				PlatinumImage.enabled = false;
				PlatinumElement.ignoreLayout = true;
			}
			else
			{
				PlatinumText.enabled = true;
				PlatinumImage.enabled = true;
				PlatinumElement.ignoreLayout = false;
			}
			if (cost.Item3 == 0)
			{
				GoldText.enabled = false;
				GoldImage.enabled = false;
				GoldElement.ignoreLayout = true;
			}
			else
			{
				GoldText.enabled = true;
				GoldImage.enabled = true;
				GoldElement.ignoreLayout = false;
			}
			if (cost.Item2 == 0)
			{
				SilverText.enabled = false;
				SilverImage.enabled = false;
				SilverElement.ignoreLayout = true;
			}
			else
			{
				SilverText.enabled = true;
				SilverImage.enabled = true;
				SilverElement.ignoreLayout = false;
			}
			if (cost.Item1 == 0)
			{
				CopperText.enabled = false;
				CopperImage.enabled = false;
				CopperElement.ignoreLayout = true;
			}
			else
			{
				CopperText.enabled = true;
				CopperImage.enabled = true;
				CopperElement.ignoreLayout = false;
			}
		}
	}
}
