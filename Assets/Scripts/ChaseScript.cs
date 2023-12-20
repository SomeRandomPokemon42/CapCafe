using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseScript : MonoBehaviour
{
	public GameObject ChaseObject;
	public float speed = 2;
	public Vector3 Offset = Vector3.zero;
	void Update()
	{
		Vector3 TrackTo = ChaseObject.transform.position - transform.position;
		TrackTo += Offset;
		Vector3 Movement = TrackTo * Time.deltaTime * speed;
		Movement = new Vector3(
			Mathf.Clamp(Movement.x, -Mathf.Abs(TrackTo.x), Mathf.Abs(TrackTo.x)),
			Mathf.Clamp(Movement.y, -Mathf.Abs(TrackTo.y), Mathf.Abs(TrackTo.y)),
			Mathf.Clamp(Movement.z, -Mathf.Abs(TrackTo.z), Mathf.Abs(TrackTo.z))
		);
		transform.Translate(Movement);
	}
}
