using System;

namespace SrtTranslator.Core
{
    public class SubtitleTimestamps
    {
        public Timestamp Start { get; private set; }
        public Timestamp End { get; private set; }

        public SubtitleTimestamps(Timestamp start, Timestamp end)
        {
            if (start == null)
                throw new ArgumentNullException(nameof(start));
            if (end == null)
                throw new ArgumentNullException(nameof(end));

            if (end.IsBefore(start))
                throw new ArgumentException();

            Start = start;
            End = end;
        }

        public override bool Equals(object obj)
        {
            return obj is SubtitleTimestamps @object &&
                Start.Equals(@object.Start) &&
                End.Equals(@object.End);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Start, End);
        }
    }
}
