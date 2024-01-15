using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TownScript : MonoBehaviour
{
	public enum Direction
	{
		up, down, left, right
	}
	//[Header("Reliance")]
	private TimeScript GameTime;
	[Header("Constant Stuff")]
	public HouseScript[] Houses;
	public Vector3[] TownPositions;
	private List<Vector3> UnusedTownPositions = new();
	[Header("Data")]
	public int CafePopularity = 0;
	public float CafeSalt = 0; // If you let salt build up too much, it reduces popularity

	public HouseScript GimmeAHouse()
	{
		return Houses[Random.Range(0, Houses.Length)];
	}

	public Vector3 GimmeSomewhereToGo()
	{
		Vector3 ReturnValue = Vector3.zero;
		if (UnusedTownPositions.Count == 0)
		{
			// What is this lambda black magic you speak of?!
			UnusedTownPositions = TownPositions.OrderBy(x => Random.value).ToList();
		}
		ReturnValue = UnusedTownPositions[Random.Range(0, UnusedTownPositions.Count)];
		UnusedTownPositions.Remove(ReturnValue);
		return ReturnValue;
	}

	private void Start()
	{
		GameTime = gameObject.GetComponent<TimeScript>();
	}

	public void Complain()
	{
		CafeSalt += 1;
	}
	public void Praise()
	{
		CafeSalt -= 0.25f;
	}

	private void Update()
	{
		if (CafeSalt > 5)
		{
			CafePopularity -= 1;
			CafeSalt -= 2.5f;
		}
		if (CafeSalt > 0)
		{
			CafeSalt -= (Time.deltaTime * GameTime.TimeSpeed / 10);
		}
		if (CafeSalt < 0)
		{
			CafeSalt += (Time.deltaTime * GameTime.TimeSpeed / 10);
		}
	}
}
