/*
Author: Richard Gaynor
*/
namespace InfinitySeriesGenerator
{
    using System.Collections.Generic;

    internal class Generator
    {
        public Generator(int startNoteIndex)
        {
            this.NoteCalculator = new NoteCalculator(startNoteIndex);
        }

        private NoteCalculator NoteCalculator { get; }

        public IEnumerable<string> GenerateSeries(int total, int start)
        {
            var numbers = new int[start * 2 + total];
            var notes = new string[start * 2 + total];
            var octave = 3;

            //0 and 1 are always the same in the series and don't adhere to the formula. They are calculated ahead of the other entries.
            numbers[0] = 0;
            if (start <= 0)
            {
                notes[0] = this.NoteCalculator.GetSeriesEntry(0, numbers[0], ref octave);
            }

            numbers[1] = 1;
            if (start <= 1)
            {
                notes[1] = this.NoteCalculator.GetSeriesEntry(1, numbers[1], ref octave);
            }

            //j is used to add to the notes array, as it sometimes won't be synced with the i in the for loop.
            //if j is 0, the counting in the for loop with j starts at 0. This would override the below code for 0 and 1 outside the for loop, so this changes j accordingly.
            var j = start <= 1 ? 1 : 0;

            this.GenerateRestOfSeries(total, start, numbers, notes, j);
            return notes;
        }

        private void GenerateRestOfSeries(int total, int start, IList<int> numbers, IList<string> notes, int j)
        {
            var limit = start + total + 1;
            for (var i = 1; i < limit; i++)
            {
                var octave = 3;
                var index = 2 * i;
                var first = index - 2;
                var second = index - 1;
                var previous = i - 1;

                //breaks if at the end
                var nextIndex = index + 1;
                if (index >= start + total || nextIndex >= start + total)
                {
                    break;
                }

                //first (even) number entry
                numbers[index] = numbers[first] - (numbers[i] - numbers[previous]);

                if (index >= start)
                {
                    
                    notes[2 * j] = this.NoteCalculator.GetSeriesEntry(index, numbers[index], ref octave);
                }

                //reset octave for next note
                octave = 3;

                //second (odd) number entry
                numbers[nextIndex] = numbers[second] + (numbers[i] - numbers[previous]);

                if (nextIndex >= start)
                {
                    notes[2 * j + 1] = this.NoteCalculator.GetSeriesEntry(nextIndex, numbers[nextIndex], ref octave);
                    j++;
                }
            }
        }
    }
}
