using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTime : MonoBehaviour, IWorldTime
{
    public static WorldTime Instance { get; private set; }

    public enum TimeOfDay
    {
        Morning,
        Afternoon,
        Evening
    }
    public enum WeekDay
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }

    [SerializeField] public float TimeScale = 360f; // 10 real second = 1 in-game hour
    [SerializeField] public float TimeMorning = 6f;
    [SerializeField] public float TimeAfternoon = 12f;
    [SerializeField] public float TimeEvening = 18f;

    private float _currentTime = 0f;
    private int _currentDay = 0; //Avoiding overflow of _currentTime float

    public float CurrentTime { get => _currentTime; }
    public float CurrentHour { get => _currentTime % 24f; }
    public int CurrentDay { get => _currentDay; }
    public TimeOfDay CurrentTimeOfDay
    {
        get
        {
            if (CurrentHour >= TimeMorning && CurrentHour < TimeAfternoon)
            {
                return TimeOfDay.Morning;
            }
            else if (CurrentHour >= TimeAfternoon && CurrentHour < TimeEvening)
            {
                return TimeOfDay.Afternoon;
            }
            else
            {
                return TimeOfDay.Evening;
            }
        }
    }

    public WeekDay CurrentWeekDay
    {
        get
        {
            return (WeekDay)(CurrentDay % 7);
        }
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Update()
    {
        _currentTime += Time.deltaTime * TimeScale / 3600f;
        if (_currentTime >= 24f)
        {
            _currentTime -= 24f;
            _currentDay++;
        }
    }

    public void AddTime(float hours)
    {
        _currentTime += hours;
        if (_currentTime >= 24f)
        {
            _currentTime -= 24f;
            _currentDay++;
        }
    }

    public void SetTime(float hours)
    {
        // If the new time is earlier than the current time, we assume it's the next day
        if(hours < CurrentHour)
        {
            _currentDay++;
        }
        _currentTime = hours * 3600f;
    }

    public void SetTime(TimeOfDay timeOfDay)
    {
        switch (timeOfDay)
        {
            case TimeOfDay.Morning:
                SetTime(TimeMorning);
                break;
            case TimeOfDay.Afternoon:
                SetTime(TimeAfternoon);
                break;
            case TimeOfDay.Evening:
                SetTime(TimeEvening);
                break;
        }
    }

    public void StartNewDay()
    {
        _currentTime = 0f;
        _currentDay++;
    }
}
