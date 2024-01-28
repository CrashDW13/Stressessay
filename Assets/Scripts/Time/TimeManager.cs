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

    private static float _currentMinute = Mathf.FloorToInt(GetMinutes());
    private static float _currentHour = Mathf.FloorToInt(GetHour());

    [SerializeField]
    private AudioClip _clockTick;
    [SerializeField]
    private AudioClip _alarmSound;
    private AudioSource _audioSource;


    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        _totalTime += Time.deltaTime / GetStressFactor();
        _currentTime = _totalTime % _dayDuration;

        if (_currentHour != Mathf.FloorToInt(GetHour()))
        {
            _audioSource.PlayOneShot(_alarmSound);
            _currentHour = Mathf.FloorToInt(GetHour());

            if (_currentHour == 12)
            {
                LevelLoader _loader = FindObjectOfType<LevelLoader>();  
                if (_loader != null)
                {
                    _loader.LoadScene("Game Over", true);
                }
            }
        }

        else if (_currentMinute != Mathf.FloorToInt(GetMinutes()))
        {
            _audioSource.PlayOneShot(_clockTick);
            _currentMinute = Mathf.FloorToInt(GetMinutes());
        }
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

    private float GetStressFactor()
    {
        return 60 * ((1 - (StressManager.GetStress() / 100) + 0.5f));
    }
}
