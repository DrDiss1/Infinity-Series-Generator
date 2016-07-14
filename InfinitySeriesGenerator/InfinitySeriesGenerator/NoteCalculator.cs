namespace InfinitySeriesGenerator
{
    using System.Collections.Generic;

    public class NoteCalculator
    {
        public static readonly IList<string> Notes =
            new List<string> { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" }.AsReadOnly();

        public NoteCalculator(int startNoteIndex)
        {
            this.StartNoteIndex = startNoteIndex;
        }

        private int StartNoteIndex { get; }

        public string GetSeriesEntry(int seriesResultIndex, int nextNoteIndex, ref int octave)
        {
            return string.Format("{0} = {1}", seriesResultIndex, this.GetNoteName(nextNoteIndex, ref octave));
        }

        /// <summary>
        ///     Calculates the next note in the series in the format [name][octave].
        /// </summary>
        private string GetNoteName(int nextNoteIndex, ref int octave)
        {
            var noteIndex = NoteCalculator.GetNextNoteIndex(nextNoteIndex + this.StartNoteIndex, ref octave);
            var note = NoteCalculator.Notes[noteIndex];
            return string.Format("{0}{1}", note, octave);
        }

        /// <summary>
        ///     Calculates the index into Notes of the next item in the series.
        ///     Adjusts octave as required while keeping the result within the bounds (0..Notes.Count).
        /// </summary>
        private static int GetNextNoteIndex(int index, ref int octave)
        {
            var number = index;
            while (number < 0 || number > 11)
            {
                if (number >= 12)
                {
                    number -= 12;
                    ++octave;
                }
                else if (number < 0)
                {
                    number += 12;
                    --octave;
                }
            }

            return number;
        }
    }
}
