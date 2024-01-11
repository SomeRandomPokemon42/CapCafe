using UnityEngine;

public class SpriteCounterAdjust : MonoBehaviour
{
	[SerializeField] private Vector3 Offset = Vector3.zero;
	void Update()
	{
		transform.LookAt(transform.position - Offset);
	}
}
