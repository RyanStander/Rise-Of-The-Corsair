using System;
using Events;
using UnityEngine;
using EventType = Events.EventType;

namespace TimeAndSeasons
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

        private void OnEnable()
        {
            EventManager.currentManager.Subscribe(EventType.NewDay, OnNewDay);
        }

        private void OnNewDay(EventData eventData)
        {
            if (!eventData.IsEventOfType(out NewDay _))
                return;

            AdvanceDay();
        }

        private void OnDisable()
        {
            EventManager.currentManager.Unsubscribe(EventType.NewDay, OnNewDay);
        }

        private void Start()
        {
            SetupCalendar();
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
            EventManager.currentManager.AddEvent(new DateChange(currentDay, currentMonth, currentSeason, currentYear));
        }

        // Method to advance the day
        private void AdvanceDay()
        {
            currentDay++;

            CheckForNewMonth();

            CheckForNewSeason();

            //Debug.Log($"Day: {currentDay} Month: {currentMonth} Season: {currentSeason} Year: {currentYear}");

            // Call event to notify date change
            EventManager.currentManager.AddEvent(new DateChange(currentDay, currentMonth, currentSeason, currentYear));
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
