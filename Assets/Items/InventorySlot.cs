using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public GameItem StoredItem = null;
	private Image ItemImage;
	private RectTransform MyTransform;
	private Image MyImage;
	void Start()
	{
		MyTransform = GetComponent<RectTransform>();
		MyImage = GetComponent<Image>();
		ItemImage = transform.GetChild(0).GetComponent<Image>();
	}
	private void Update()
	{
		if (StoredItem != null)
		{
			ItemImage.enabled = true;
			ItemImage.sprite = StoredItem.Icon;
		}
		else
		{
			ItemImage.enabled = false;
		}
	}
	public void OnPointerEnter(PointerEventData eventData)
	{
		Debug.Log("Over");
		MyImage.color = Color.yellow;
	}
	public void OnPointerExit(PointerEventData eventData)
	{
		Debug.Log("Out");
		MyImage.color = Color.white;
	}
}
