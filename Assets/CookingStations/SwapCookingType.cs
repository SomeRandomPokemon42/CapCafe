using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static RecipeBook;

public class SwapCookingType : MonoBehaviour
{
	private CookingScript cookingScript;
	public CookingType cookingType;
	private void Start()
	{
		cookingScript = transform.parent.GetComponentInParent<CookingScript>();
		GetComponent<Toggle>().onValueChanged.AddListener(Changed);
	}
	private void Changed(bool NewValue)
	{
		if (NewValue)
		{
			cookingScript.cookingType = cookingType;
			cookingScript.PreviewCooking();
		}
	}
}
