using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TimeFormatter = CatAttack.TimeFormatter;

public class TimeFormattingTest : MonoBehaviour
{
    private float[] testTimes = { 30f, 66.666666f, 128.0640f, 0.5f, 60.050f };

    // Start is called before the first frame update
    public void Start()
    {
        foreach (float time in testTimes)
        {
            TimeFormatter formatter = new TimeFormatter(time);
            Debug.Log(string.Format(
                "Debugging time: {0}\n  minutes: {1}, seconds: {2}, ms: {3}\n  string: {4}",
                time,
                formatter.minutes,
                formatter.seconds,
                formatter.milliseconds,
                formatter.toString
            ));
        }
    }    
}
