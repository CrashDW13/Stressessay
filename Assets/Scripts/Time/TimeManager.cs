using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private const int _hoursInDay = 24;
    private const int _minutesInHour = 60;

    private static float _dayDuration = 30f;

    private static float _totalTime = 12.5f;
    private static float _currentTime = 12.5f;


    private void Update()
    {
        _totalTime += Time.deltaTime / 60;
        _currentTime = _totalTime % _dayDuration;
    }

    public static float GetHour()
    {
        return _currentTime * _hoursInDay / _dayDuration;
    }

    public static float GetMinutes()
    {
        return (_currentTime * _hoursInDay * _minutesInHour / _dayDuration) % _minutesInHour;
    }

    public static string GetClockString()
    {
        int hour = Mathf.FloorToInt(GetHour());
        string abbreviation = "PM";

        return hour.ToString("00") + ":" + Mathf.FloorToInt(GetMinutes()).ToString("00") + " " + abbreviation;
    }
}
