using UnityEngine;
using System;

namespace Environment
{
    /// <summary>
    ///
    /// </summary>
    public class DateCalendar : MonoBehaviour
    {
        #region Serialized Fields

        [SerializeField] private int startingYear = 1678;
        [SerializeField] private Season startingSeason = Season.Spring;

        [SerializeField,
         Tooltip("The offset makes it so that the first day the game starts is " +
                 "not the 1st day of the season so that it feels more natural")]
        private int startingDayOffset = 16;

        #endregion

        #region Private Variables

        private int currentYear;
        private Season currentSeason;
        private int currentMonth;
        private int currentDay;
        private int totalDaysInMonth;

        #endregion

        // Event for handling date changes
        public static event Action<int, Season, int, int> OnDateChange;

        private void Start()
        {
            SetupCalendar();
        }

        private void Update()
        {
            // Simulate time progression (you can replace this with your time handling logic)
            if (Input.GetKeyDown(KeyCode.Space))
            {
                AdvanceDay();
            }
        }

        private void SetupCalendar()
        {
            currentYear = startingYear;
            currentSeason = startingSeason;

            //get the current month from the current season
            currentMonth = DateTimeUtilities.GetStartingMonthFromSeason(currentSeason);

            totalDaysInMonth = DateTime.DaysInMonth(currentYear, currentMonth);

            currentDay = 21 + startingDayOffset;

            CheckForNewMonth();

            CheckForNewSeason();

            // Call event to notify initial date
            OnDateChange?.Invoke(currentYear, currentSeason, currentMonth, currentDay);
        }

        // Method to advance the day
        private void AdvanceDay()
        {
            currentDay++;

            CheckForNewMonth();

            CheckForNewSeason();

            Debug.Log("Current Date: " + currentYear + " " + currentSeason + " " + currentMonth + " " + currentDay);

            // Call event to notify date change
            OnDateChange?.Invoke(currentYear, currentSeason, currentMonth, currentDay);
        }

        /// <summary>
        /// This method will keep checking for a new month until the total days in the month is greater than the current day.
        /// </summary>
        private void CheckForNewMonth()
        {
            //While loop exists as a precaution in the event that the starting days are incredibly high
            while (true)
            {
                if (currentDay > totalDaysInMonth)
                {
                    var daysOver = currentDay - totalDaysInMonth;
                    currentDay = daysOver;

                    currentMonth++;

                    if (currentMonth > 12)
                    {
                        currentMonth = 1;
                        currentYear++;
                    }

                    totalDaysInMonth = DateTime.DaysInMonth(currentYear, currentMonth);

                    continue;
                }

                break;
            }
        }

        private void CheckForNewSeason()
        {
            if (currentDay is > 20 and < 25)
            {
                currentSeason = DateTimeUtilities.GetSeason(currentMonth, currentDay);
            }
        }
    }
}
