using System;

namespace RxLocal.Core
{
    public static class TypeExtensions
    {
        public static bool IsNullOrDefault<T>(this T? value) where T : struct
        {
            return default(T).Equals(value.GetValueOrDefault());
        }
    }

    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }
    }

    public static class DateTimeExtensions
    {
        /// <summary>
        /// Get a DateTime that represents the beginning of the hour of the provided date.
        /// </summary>
        /// <param name="date">The date</param>
        /// <returns></returns>
        public static DateTime BeginningOfHour(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, date.Hour, 0, 0, 0, date.Kind);
        }

        /// <summary>
        /// Get a DateTime that represents the end of the hour of the provided date.
        /// </summary>
        /// <param name="date">The date</param>
        /// <returns></returns>
        public static DateTime EndOfHour(this DateTime date)
        {
            return date.BeginningOfHour().AddHours(1).AddTicks(-1);
        }

        /// <summary>
        /// Get a DateTime that represents the beginning of the day of the provided date.
        /// </summary>
        /// <param name="date">The date</param>
        /// <returns></returns>
        public static DateTime BeginningOfDay(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0, date.Kind);
        }

        /// <summary>
        /// Get a DateTime that represents the end of the day of the provided date.
        /// </summary>
        /// <param name="date">The date</param>
        /// <returns></returns>
        public static DateTime EndOfDay(this DateTime date)
        {
            return date.BeginningOfDay().AddDays(1).AddTicks(-1);
        }

        /// <summary>
        /// Get a DateTime that represents the beginning of the month of the provided date.
        /// </summary>
        /// <param name="date">The date</param>
        /// <returns></returns>
        public static DateTime BeginningOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1, 0, 0, 0, 0, date.Kind);
        }

        /// <summary>
        /// Get a DateTime that represents the end of the month of the provided date.
        /// </summary>
        /// <param name="date">The date</param>
        /// <returns></returns>
        public static DateTime EndOfMonth(this DateTime date)
        {
            return date.BeginningOfMonth().AddMonths(1).AddTicks(-1);
        }

        /// <summary>
        /// Get a DateTime that represents the beginning of the year of the provided date.
        /// </summary>
        /// <param name="date">The date</param>
        /// <returns></returns>
        public static DateTime BeginningOfYear(this DateTime date)
        {
            return new DateTime(date.Year, 1, 1, 0, 0, 0, 0, date.Kind);
        }
         
        /// <summary>
        /// Get a DateTime that represents the end of the year of the provided date.
        /// </summary>
        /// <param name="date">The date</param>
        /// <returns></returns>
        public static DateTime EndOfYear(this DateTime date)
        {
            return date.BeginningOfYear().AddYears(1).AddTicks(-1);
        }

        /// <summary>
        /// Determines if the provided datetime is a weekend.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool IsWeekend(this DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
        }

        ///<summary>
        /// Gets the first week day following a date. The current date is not included in the comparison.
        /// </summary>
        ///<param name="date">The date.</param>
        ///<param name="dayOfWeek">The day of week to return.</param>
        ///<returns>The first occurrence of the provided dayOfWeek day following the provided date.</returns>
        public static DateTime Next(this DateTime date, DayOfWeek dayOfWeek)
        {
            return date.AddDays((dayOfWeek <= date.DayOfWeek ? 7 : 0) + dayOfWeek - date.DayOfWeek);
        }
    }
}
