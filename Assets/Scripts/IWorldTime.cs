public interface IWorldTime
{
    float CurrentTime { get; }
    float CurrentHour { get; }
    int CurrentDay { get; }
    WorldTime.TimeOfDay CurrentTimeOfDay { get; }

    void AddTime(float hours);
    void SetTime(float hours);
    void SetTime(WorldTime.TimeOfDay timeOfDay);

    void StartNewDay();
}