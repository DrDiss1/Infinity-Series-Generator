# Infinity-Series-Generator
A small program in C# that creates a list of notes for Per Nørgård's Infinity Series, and outputs it as a text file.

What is the Infinity Series?
Here's a quote:
"Nørgård’s infinite series is actually an integer sequence produced by a relatively simple algorithm that “unpacks” a single musical interval. A single interval is all you need to generate an unstoppable Nørgård sequence. Say you want to begin a piece with the melody G - A. Let’s assume just white notes (diatonic) are in use. From this melody, Nørgård would extract an essential piece of info, the ascending +1 “go up by one” interval between G and A. Then, he composes out that interval, using its inverted form as instructions on what the next pitch shall be: go -1 away from G. Thus, the 3rd note in the sequence is 1 below G, or F. He does the same for A, only with the original interval, so that the 4th note in the series is +1 from A, or B. We’ve got a nice little tune, already fanning away from G! The instructions we’re following are essentially: take each new interval that appears in the sequence starting at the front, and go that far (in inversion) from the second to last note in sequence, and then go that far (uninverted) from the last note in the sequence."
Found here: http://unsungsymphonies.blogspot.com.au/2010/08/master-of-infinite-series-nrgards.html

This program creates the Infinity Series for you, with the starting interval being a semitone.

There are 3 main C# files used:

# Generator
Currently only functions with a semitone as the first interval. Might add other options, like diatonic series, or different starting intervals later.

# KeyRules
A set of conditions for key presses, such as checking for enter, checking for number keys, and so on.

# MainWindow
The file housing the GUI provided by Visual Studio, and the relevant inputs. Calls iGenerator when told to create the Infinity Series.
