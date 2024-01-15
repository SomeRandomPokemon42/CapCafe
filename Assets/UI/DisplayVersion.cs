using TMPro;
using UnityEngine;

public class DisplayVersion : MonoBehaviour
{
	void Start()
	{
		gameObject.GetComponent<TextMeshProUGUI>().text = Application.version;
	}
}
