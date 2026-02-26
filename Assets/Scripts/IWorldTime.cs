public interface IWorldTime
{
    float CurrentTime { get; }
    float CurrentHour { get; }
    int CurrentDay { get; }
    WorldTime.TimeOfDay CurrentTimeOfDay { get; }
    WorldTime.WeekDay CurrentWeekDay { get; }

    void AddTime(float hours);
    void AddTime(int days);
    void SetTime(float hours);
    void SetTime(int day);
    void SetTime(WorldTime.TimeOfDay timeOfDay);
    void SetTime(WorldTime.WeekDay weekDay);

    void StartNewDay();
}