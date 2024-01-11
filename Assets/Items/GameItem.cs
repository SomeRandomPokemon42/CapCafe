using UnityEngine;

[CreateAssetMenu]
public class GameItem : ScriptableObject
{
	public string Name;
	public Sprite Icon;
	[Header("Merchant")]
	public int BaseValue = 0;
	public bool Sellable = true;
	[Header("Tags")]
	public bool KeyItem = false;
	public bool Orderable = false;
	public bool IsBeverage = false;
	[Header("Contents")]
	public bool HasAlcohol = false;
	public bool HasMeat = false;
	public bool HasMilk = false;

	public GameItem Clone()
	{
		return (GameItem)MemberwiseClone();
	}
}
