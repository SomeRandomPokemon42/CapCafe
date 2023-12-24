using TMPro;
using UnityEngine;

public class ClockUIScript : MonoBehaviour
{
	// This is my attempt at making a very reusable and segmented script
	[Header("Time")]
	public TimeScript timescript;
	private int hour;
	private int minute;
	private int lastminute = 99;
	private int lasthour = 99;
	[Header("Analog")]
	[SerializeField] bool AnalogClock = false;
	[SerializeField] GameObject MinuteHand;
	[SerializeField] GameObject HourHand;
	[Header("Digital")]
	[SerializeField] bool DigitalClock = false;
	[SerializeField] TextMeshProUGUI TextMesh;
	[SerializeField] bool Do24HourTime = true;

	void Start()
	{
		// if you've been ignoring these warnings, then F*** your build, can't fix stupid.
		#if UNITY_EDITOR
		if (DigitalClock && TextMesh == null)
		{
			DigitalClock = false;
			Debug.LogWarning("Cannot enable Digital Clock without a text mesh");
		}
		if (AnalogClock && MinuteHand == null && HourHand == null)
		{
			AnalogClock = false;
			Debug.LogWarning("Cannot enable Analog Clock without an hour or minute hand");
		}
		# endif
		if (lasthour == hour)
		{
			lasthour += 99;
		}
		if (lastminute == minute)
		{
			lastminute += 99;
		}
	}

	private void updateDigital(int inphour, int inpminute, bool doAMPM)
	{
		string zsuffix = "";
		string zhour = inphour.ToString();
		string zminute = inpminute.ToString();
		if (doAMPM)
		{
			if (inphour > 12)
			{
				zsuffix = " PM";
			} else
			{
				zsuffix = " AM";
			}
			zhour = (inphour - 12).ToString();
			inphour -= 12;
		}
		if (inphour < 10)
		{
			zhour = "0" + inphour.ToString();
		}
		if (inpminute < 10)
		{
			zminute = "0" + inpminute.ToString();
		}
		TextMesh.text = zhour + ":" + zminute + zsuffix;
	}
	private void updateAnalog(bool HasMinute, bool HasHour, int inphour, int inpminute)
	{
		if (HasMinute)
		{
			float PositionOfMinute = inpminute / 60f * 360f;
			MinuteHand.transform.rotation = Quaternion.Euler(0, 0, 0 - PositionOfMinute);
		}
		if (HasHour)
		{
			float PositionOfHour = inphour / 12f * 360f;
			PositionOfHour += inpminute / 2f;
			HourHand.transform.rotation = Quaternion.Euler(0, 0, 0 - PositionOfHour);
		}
	}
	// Update is called once per frame
	void Update()
	{
		minute = timescript.minute;
		hour = timescript.hour;
		if (lasthour != hour || lastminute != minute)
		{
			lasthour = hour;
			lastminute = minute;
			if (DigitalClock)
			{
				updateDigital(hour, minute, !Do24HourTime);
			}
			if (AnalogClock)
			{
				updateAnalog(HourHand != null, MinuteHand != null, hour, minute);
			}
		}
	}
}
