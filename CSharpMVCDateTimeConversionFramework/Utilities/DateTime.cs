using System;


namespace CSharpMVCDateTimeConversionFramework.Utilities 
{
    public static class DateTimeExtensions 
    {
        public static DateTime ToLocalTime(this DateTime utcDateTime, TimeZoneInfo timeZoneInfo) {
            var date = DateTime.SpecifyKind(utcDateTime, DateTimeKind.Unspecified);
            return TimeZoneInfo.ConvertTimeFromUtc(date, timeZoneInfo);
        }

        public static DateTime ToLocalTime(this DateTime utcDateTime, string timeZoneId) {
            var date = DateTime.SpecifyKind(utcDateTime, DateTimeKind.Unspecified);
            return TimeZoneInfo.ConvertTimeFromUtc(date, TimeZoneInfo.FindSystemTimeZoneById(timeZoneId));
        }

        public static DateTime ToUniversalTime(this DateTime localDateTime, TimeZoneInfo timeZoneInfo) {
            var date = DateTime.SpecifyKind(localDateTime, DateTimeKind.Unspecified);
            return TimeZoneInfo.ConvertTimeToUtc(date, timeZoneInfo);
        }

        public static DateTime ToUniversalTime(this DateTime localDateTime, string timeZoneId) {
            var date = DateTime.SpecifyKind(localDateTime, DateTimeKind.Unspecified);
            return TimeZoneInfo.ConvertTimeToUtc(date, TimeZoneInfo.FindSystemTimeZoneById(timeZoneId));
        }

        public static DateTime GetFirstDayOfMonth(this DateTime datetime) {
            return new DateTime(datetime.Year, datetime.Month, 1);
        }

        public static DateTime GetLastDayOfMonth(this DateTime datetime) {
            return GetFirstDayOfMonth(datetime).AddMonths(1).AddDays(-1);
        }

        public static DateTime GetFirstDayOfWeek(this DateTime datetime) {
            while (datetime.DayOfWeek != DayOfWeek.Sunday)
                datetime = datetime.AddDays(-1);

            return datetime;
        }

        public static DateTime GetLastDayOfWeek(this DateTime datetime) {
            return GetFirstDayOfWeek(datetime).AddDays(6);
        }

        public static DateTime GetStartOfDay(this DateTime datetime) {
            return new DateTime(datetime.Year, datetime.Month, datetime.Day, 0, 0, 0, 0);
        }

        public static DateTime GetEndOfDay(this DateTime datetime) {
            return GetStartOfDay(datetime).AddDays(1).AddMilliseconds(-1);
        }

        public static long GetEpochDateTime(this DateTime dateTime) {
            return (long)(dateTime - new DateTime(1970, 1, 1)).TotalSeconds;
        }

        public static DateTime ParseEpochDateTime(long epochDateTime) {
            return new DateTime(1970, 1, 1).AddSeconds(epochDateTime);
        }

        public static TimeSpan Hours(this int value) {
            return new TimeSpan(value, 0, 0);
        }

        public static TimeSpan Minutes(this int value) {
            return new TimeSpan(0, value, 0);
        }

        public static TimeSpan Seconds(this int value) {
            return new TimeSpan(0, 0, value);
        }
    }
}