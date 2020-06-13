using System;

namespace SrtTranslator.Core
{
    public class Subtitle
    {
        public SubtitleTimestamps Timestamps { get; private set; }
        public SubtitleText Text { get; private set; }

        public Subtitle(SubtitleTimestamps timestamps, SubtitleText text)
        {
            if (timestamps == null)
                throw new ArgumentNullException(nameof(timestamps));
            if (text == null)
                throw new ArgumentNullException(nameof(text));

            Timestamps = timestamps;
            Text = text;
        }

        public override bool Equals(object obj)
        {
            return obj is Subtitle @object &&
                Timestamps.Equals(@object.Timestamps) &&
                Text.Equals(@object.Text);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Timestamps, Text);
        }
    }
}
