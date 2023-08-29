//Event that informs subscribers of a debug log

using TimeAndSeasons;
using Weather;

namespace Events
{
    public class DateChange : EventData
    {
        public readonly int CurrentDay;
        public readonly int CurrentMonth;
        public readonly Season CurrentSeason;
        public readonly int CurrentYear;

        public DateChange(int currentDay, int currentMonth, Season currentSeason, int currentYear) : base(
            EventType.DateChange)
        {
            CurrentDay = currentDay;
            CurrentMonth = currentMonth;
            CurrentSeason = currentSeason;
            CurrentYear = currentYear;
        }
    }

    public class SendTimeStrength : EventData
    {
        public readonly float TimeStrength;
        public SendTimeStrength(float timeStrength) : base(EventType.SendTimeStrength)
        {
            TimeStrength = timeStrength;
        }
    }

    public class NewDay : EventData
    {
        public NewDay():base(EventType.NewDay)
        {

        }
    }

    public class WeatherHasChanged : EventData
    {
        public readonly WeatherPreset WeatherPreset;
        public WeatherHasChanged(WeatherPreset weatherPreset) : base(EventType.WeatherHasChanged)
        {
            WeatherPreset = weatherPreset;
        }
    }
}
