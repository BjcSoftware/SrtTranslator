using System;

namespace SubtitleFileParser.Core
{
    public class Timestamp
    {
        public HoursTimestamp Hours { get; private set; }
        public MinutesTimestamp Minutes { get; private set; }
        public SecondsTimestamp Seconds { get; private set; }
        public MillisecondsTimestamp Milliseconds { get; private set; }

        public Timestamp(
            HoursTimestamp hours, 
            MinutesTimestamp minutes, 
            SecondsTimestamp seconds, 
            MillisecondsTimestamp milliseconds)
        {
            if (hours == null)
                throw new ArgumentNullException(nameof(hours));
            if (minutes == null)
                throw new ArgumentNullException(nameof(minutes));
            if (seconds == null)
                throw new ArgumentNullException(nameof(seconds));
            if (milliseconds == null)
                throw new ArgumentNullException(nameof(milliseconds));

            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;
            Milliseconds = milliseconds;
        }

        public bool IsBefore(Timestamp other)
        {
            string this_string = $"{Hours.Value:00}{Minutes.Value:00}{Seconds.Value:00}{Milliseconds.Value:000}";
            string other_string = $"{other.Hours.Value:00}{other.Minutes.Value:00}{other.Seconds.Value:00}{other.Milliseconds.Value:000}";

            return StringComparer.OrdinalIgnoreCase.Compare(this_string, other_string) < 0;
        }

        public override bool Equals(object obj)
        {
            return obj is Timestamp @object &&
                Hours.Equals(@object.Hours) &&
                Minutes.Equals(@object.Minutes) &&
                Seconds.Equals(@object.Seconds) &&
                Milliseconds.Equals(@object.Milliseconds);
        }

        public override int GetHashCode()
        {
            return
                Hours.GetHashCode() ^
                Minutes.GetHashCode() ^
                Seconds.GetHashCode() ^
                Milliseconds.GetHashCode();
        }
    }
}
