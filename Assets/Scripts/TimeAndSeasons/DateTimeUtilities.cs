using System;

namespace TimeAndSeasons
{
    public static class DateTimeUtilities
    {
        /// <summary>
        /// Returns the season based on the month and day.
        /// </summary>
        public static Season GetSeason(int month, int day)
        {
            var value = month + day / 100f; // <month>.<day(2 digit)>
            if (value < 3.21 || value >= 12.22) return Season.Winter;
            if (value < 6.21) return Season.Spring;
            return value < 9.23 ? Season.Summer :
                Season.Autumn;
        }

        public static int GetStartingMonthFromSeason(Season season)
        {
            switch (season)
            {
                case Season.Spring:
                    return 3;
                case Season.Summer:
                    return 6;
                case Season.Autumn:
                    return 9;
                case Season.Winter:
                    return 12;
                default:
                    throw new ArgumentOutOfRangeException(nameof(season), season, null);
            }
        }
    }
}
