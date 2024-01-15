using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardScript : MonoBehaviour
{
	[SerializeField] Camera cam = null;
	[SerializeField] Vector3 offset = Vector3.zero;
	// Start is called before the first frame update
	void Start()
	{
		if (cam == null)
		{
			cam = Camera.main;
		}
	}

	// Update is called once per frame
	void Update()
	{
		transform.LookAt(cam.transform.position + offset);
		transform.Rotate(new Vector3(0, 180, 0));
	}
}
