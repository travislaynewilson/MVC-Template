using System;
using RxLocal.Tests;
using NUnit.Framework;

namespace RxLocal.Core.Tests
{
    [TestFixture]
    public class ExtensionsTests
    {
        [Test]
        public void IsNullOrDefault()
        {
            int? x1 = null;
            x1.IsNullOrDefault().ShouldBeTrue();

            int? x2 = 0;
            x2.IsNullOrDefault().ShouldBeTrue();

            int? x3 = 1;
            x3.IsNullOrDefault().ShouldBeFalse();
        }

        [Test]
        public void String_IsNullOrEmpty()
        {
            ("").IsNullOrEmpty().ShouldBeTrue();

            string.Empty.IsNullOrEmpty().ShouldBeTrue();

            "some value".IsNullOrEmpty().ShouldBeFalse();
        }

        [Test]
        public void DateTime_BeginningOfHour()
        {
            var source = new DateTime(2000, 1, 1, 5, 45, 9);
            var result = source.BeginningOfHour();

            result.Hour.ShouldEqual(5);
            source.Subtract(result).TotalHours.ShouldBeLessThan(1);
        }

        [Test]
        public void DateTime_EndOfHour()
        {
            var source = new DateTime(2000, 1, 1, 5, 0, 0);
            var result = source.EndOfHour();

            result.Hour.ShouldEqual(5);
            result.ShouldBeLessThan(source.AddHours(1));
        }

        [Test]
        public void DateTime_BeginningOfDay()
        {
            var source = new DateTime(2000, 1, 1, 5, 45, 9);
            var result = source.BeginningOfDay();

            result.Day.ShouldEqual(1);
            source.Subtract(result).TotalDays.ShouldBeLessThan(1);
        }

        [Test]
        public void DateTime_EndOfDay()
        {
            var source = new DateTime(2000, 1, 1, 5, 0, 0);
            var result = source.EndOfDay();

            result.Day.ShouldEqual(1);
            result.ShouldBeLessThan(source.AddDays(1));
        }

        [Test]
        public void DateTime_BeginningOfMonth()
        {
            var source = new DateTime(2000, 1, 15, 1, 0, 0);
            var result = source.BeginningOfMonth();

            result.Year.ShouldEqual(2000);
            result.Month.ShouldEqual(1);
            result.Day.ShouldEqual(1);
        }

        [Test]
        public void DateTime_EndOfMonth()
        {
            var source = new DateTime(2000, 1, 1, 1, 0, 0);
            var result = source.EndOfMonth();

            result.Month.ShouldEqual(1);
            result.ShouldBeLessThan(source.AddMonths(1));
        }

        [Test]
        public void DateTime_BeginningOfYear()
        {
            var source = new DateTime(2000, 6, 1, 1, 0, 0);
            var result = source.BeginningOfYear();

            result.Year.ShouldEqual(2000);
            result.Month.ShouldEqual(1);
            result.Day.ShouldEqual(1);
        }

        [Test]
        public void DateTime_EndOfYear()
        {
            var source = new DateTime(2000, 1, 1, 1, 0, 0);
            var result = source.EndOfYear();

            result.Year.ShouldEqual(2000);
            result.ShouldBeLessThan(source.AddYears(1));
        }

        [Test]
        public void DateTime_IsWeekend()
        {
            new DateTime(2015, 9, 7).IsWeekend().ShouldBeFalse(); // Monday
            new DateTime(2015, 9, 8).IsWeekend().ShouldBeFalse(); // Tuesday
            new DateTime(2015, 9, 9).IsWeekend().ShouldBeFalse(); // Wednesday
            new DateTime(2015, 9, 10).IsWeekend().ShouldBeFalse(); // Thursday
            new DateTime(2015, 9, 11).IsWeekend().ShouldBeFalse(); // Friday
            new DateTime(2015, 9, 12).IsWeekend().ShouldBeTrue(); // Saturday
            new DateTime(2015, 9, 13).IsWeekend().ShouldBeTrue(); // Sunday
        }

        [Test]
        public void DateTime_Next()
        {
            var source = new DateTime(2015, 9, 7); // Monday

            source.Next(DayOfWeek.Tuesday).Subtract(source).TotalDays.ShouldEqual(1);
            source.Next(DayOfWeek.Wednesday).Subtract(source).TotalDays.ShouldEqual(2);
            source.Next(DayOfWeek.Thursday).Subtract(source).TotalDays.ShouldEqual(3);
            source.Next(DayOfWeek.Friday).Subtract(source).TotalDays.ShouldEqual(4);
            source.Next(DayOfWeek.Saturday).Subtract(source).TotalDays.ShouldEqual(5);
            source.Next(DayOfWeek.Sunday).Subtract(source).TotalDays.ShouldEqual(6);
            source.Next(DayOfWeek.Monday).Subtract(source).TotalDays.ShouldEqual(7);
        }
    }
}
