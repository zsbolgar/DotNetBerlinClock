using System;
using System.Text;
using System.Text.RegularExpressions;

namespace BerlinClock.Classes
{
    public class TimeConverter : ITimeConverter
    {
        private const char BlankLed = 'O';
        private const char YellowLed = 'Y';
        private const char RedLed = 'R';

        public string convertTime(string aTime)
        {
            //if the method is not called from a step definition with an appropriate parameter matching rule
            if (! Regex.IsMatch(aTime, @"^(?:[01]\d|2[0-3]):(?:[0-5]\d):(?:[0-5]\d)$|^(?:24:00:00)$"))
                throw new FormatException($"{aTime} has invalid format. The valid format is: HH:mm:ss");

            var timeElements = aTime.Split(':');

            var result = new StringBuilder();
            result.AppendLine(BuildSeconds(int.Parse(timeElements[2])));
            result.AppendLine(BuildHours(int.Parse(timeElements[0])));
            result.Append((BuildMinutes(int.Parse(timeElements[1]))));
            return result.ToString();
        }

        private string BuildMinutes(int minutes)
        {
            var minutesLeds = new StringBuilder();
            var ledsOfFives = minutes / 5;
            var ledsOfOnes = minutes % 5;

            for (var i = 1; i <= ledsOfFives; i++)
            {
                minutesLeds.Append(i % 3 == 0 ? RedLed : YellowLed);
            }

            minutesLeds.AppendLine(new string(BlankLed, 11 - ledsOfFives));
            minutesLeds.Append(GetOneUnitLeds(YellowLed, ledsOfOnes));
            return minutesLeds.ToString();
        }

        private string BuildHours(int hours)
        {
            var hoursLeds = new StringBuilder();
            var ledsOfFives = hours / 5;
            var ledsOfOnes = hours % 5;

            hoursLeds.Append(RedLed, ledsOfFives);
            hoursLeds.AppendLine(new string(BlankLed, 4 - ledsOfFives));
            hoursLeds.Append(GetOneUnitLeds(RedLed, ledsOfOnes));
            return hoursLeds.ToString();
        }

        private string BuildSeconds(int sec)
        {
            return sec % 2 != 1 ? YellowLed.ToString() : BlankLed.ToString();
        }

        private string GetOneUnitLeds(char onLedChar, int count)
        {
            var ledsOn = new string(onLedChar, count);
            var ledsOff = new string(BlankLed, 4 - count);
            return ledsOn + ledsOff;
        }
    }
}
