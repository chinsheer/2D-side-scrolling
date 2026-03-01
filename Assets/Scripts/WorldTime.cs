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

    private float _currentTime = 0f; // In seconds
    private int _currentDay = 0; //Avoiding overflow of _currentTime float

    public float CurrentTime { get => _currentTime; }
    public float CurrentHour { get => _currentTime / 3600f; }
    public int CurrentDay { get => _currentDay; }

    public event System.Action<int> OnDayChanged;

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
        _currentTime += Time.deltaTime * TimeScale;
        if (CurrentHour >= 24f)
        {
            StartNewDay();
        }
    }

    public void AddTime(float hours)
    {
        _currentTime += hours * 3600f;
        if (CurrentHour >= 24f)
        {
            StartNewDay();
        }
    }

    public void AddTime(int days)
    {
        _currentDay += days;
        OnDayChanged?.Invoke(_currentDay);
    }

    public void SetTime(float hours)
    {
        // If the new time is earlier than the current time, we assume it's the next day
        if(hours < CurrentHour)
        {
            _currentDay++;
            OnDayChanged?.Invoke(_currentDay);
        }
        _currentTime = hours * 3600f;
    }

    public void SetTime(int day)
    {
        _currentDay = day;
        OnDayChanged?.Invoke(_currentDay);
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

    public void SetTime(WeekDay weekDay)
    {
        // Shift to the next occurrence of the specified weekday (won't go back)
        _currentDay += ((int)weekDay + 7 - (int)CurrentWeekDay) % 7; 
    }


    public void StartNewDay()
    {
        _currentTime = 0f;
        _currentDay++;
        OnDayChanged?.Invoke(_currentDay);
    }
}
