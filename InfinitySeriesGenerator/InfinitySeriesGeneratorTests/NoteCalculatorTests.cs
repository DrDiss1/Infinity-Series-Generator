namespace InfinitySeriesGenerator.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class NoteCalculatorTests
    {
        private NoteCalculator calculator;
        private int octave;

        [SetUp]
        public void Setup()
        {
            //start at C4
            var startNoteIndex = 0;
            this.octave = 4;
            this.calculator = new NoteCalculator(startNoteIndex);
        }

        [Test]
        public void TestFirstEntries()
        {
            var first = calculator.GetSeriesEntry(0, 0, ref octave);
            Assert.That(first, Is.EqualTo(string.Format("{0} = {1}", 0, "C4")), "expected note mismatch");

            var second = calculator.GetSeriesEntry(1, 1, ref octave);
            Assert.That(second, Is.EqualTo(string.Format("{0} = {1}", 1, "C#4")), "expected note mismatch");
        }

        [Test]
        public void TestThirdEntry()
        {
            var third = calculator.GetSeriesEntry(2, 2, ref octave);
            Assert.That(third, Is.EqualTo(string.Format("{0} = {1}", 0, "B3")), "expected note mismatch");
        }
    }
}
