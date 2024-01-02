using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScript : MonoBehaviour
{
    public int hour = 0;
    public int minute = 0;
    public float second = 0;
    public float TimeSpeed = 1;

    // Update is called once per frame
    void Update()
    {
        second += Time.deltaTime * TimeSpeed;
        if (second >= 60)
        {
            second -= 60;
            minute += 1;
        }
        if (minute >= 60)
        {
            minute -= 60;
            hour += 1;
        }
    }
}
