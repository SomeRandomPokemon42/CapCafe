using System.Collections;
using System.Collections.Generic;
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
            // Get money
            int copper = money.amount;
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
            // Write to TextMesh
            CopperText.text = ConvertNumberToString2(copper);
            SilverText.text = ConvertNumberToString2(silver);
            GoldText.text = ConvertNumberToString2(gold);
            PlatinumText.text = ConvertNumberToString2(platinum);
        }
    }
}
