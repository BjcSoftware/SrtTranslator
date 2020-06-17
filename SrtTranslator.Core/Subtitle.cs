using System;

namespace SrtTranslator.Core
{
    public class Subtitle
    {
        public SubtitleId Id { get; private set; }
        public SubtitleTimestamps Timestamps { get; private set; }
        public SubtitleText Text { get; private set; }

        public Subtitle(
            SubtitleId id,
            SubtitleTimestamps timestamps, 
            SubtitleText text)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));
            if (timestamps == null)
                throw new ArgumentNullException(nameof(timestamps));
            if (text == null)
                throw new ArgumentNullException(nameof(text));

            Id = id;
            Timestamps = timestamps;
            Text = text;
        }

        public override bool Equals(object obj)
        {
            return obj is Subtitle @object &&
                Id.Equals(@object.Id) &&
                Timestamps.Equals(@object.Timestamps) &&
                Text.Equals(@object.Text);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Timestamps, Text);
        }
    }
}
