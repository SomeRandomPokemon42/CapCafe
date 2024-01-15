using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonPeople : MonoBehaviour
{
	// Bones
	private TimeScript GameTime;
	[Header("Customers")]
	[SerializeField] GameObject[] Humans;
	[SerializeField] AnimationCurve HumanRate;
	[SerializeField] private float PersonCooldown;
	private float CurrentHeat;
	[SerializeField] private float Modfier = 1;
	// Start is called before the first frame update
	void Start()
	{
		GameTime = GetComponent<TimeScript>();
	}

	// Update is called once per frame
	void Update()
	{
		CurrentHeat = HumanRate.Evaluate(GameTime.hour + GameTime.minute / 60) * Modfier;
		PersonCooldown += CurrentHeat * Time.deltaTime * Random.value;

		if (PersonCooldown > 100)
		{
			PersonCooldown -= 100;
			Instantiate(Humans[Random.Range(0, Humans.Length)], new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
		}
	}
}
