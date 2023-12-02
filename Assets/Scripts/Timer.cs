using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    TimeSpan time;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += TimeSpan.FromSeconds(Time.deltaTime);
        Debug.Log(string.Format("<{0}:{1}::{2}", time.Minutes, time.Seconds, time.Milliseconds));
    }
}
