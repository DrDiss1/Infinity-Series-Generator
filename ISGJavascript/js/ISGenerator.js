/**
 * Created by Richard on 12/12/2016.
 */

var octave;
var octaveLowLimit = -2;
var octaveHighLimit = 8;
var currentNote;

var notes = ["C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B"];

var $generateButton = $("#generateButton");
var $noteChoice = $("#noteChoice");
var $octChoice = $("#octChoice");
var $amountOfNotes = $("#amountOfNotes");
var $noteStart = $("#noteStart");

//for when the generate button is clicked!
$generateButton.on('click', function(){
    //console.log("Number = " + $amountOfNotes.val() + ", note = " +  $noteChoice.find("option:selected").text() + $noteChoice.val() + ", octave = " + $octChoice.val());
    currentNote = $noteChoice.val();
    console.log(Generate(parseInt($amountOfNotes.val()),parseInt($noteStart.val()),parseInt($octChoice.val())));
});

//makes sure the number limit is held up for the amount of notes.
$amountOfNotes.on('change', function(){
    if($amountOfNotes.val() <= 0) {
        $amountOfNotes.val(0);
    } else if($amountOfNotes.val() >= 1000){
        $amountOfNotes.val(1000);
    }
});

/**
 * @return {string}
 */
function GetSeriesEntry(seriesResultIndex, nextNoteIndex)
{
    return seriesResultIndex + " = " + GetNoteName(nextNoteIndex);
}

/// <summary>
///     Calculates the next note in the series in the format [name][octave].
/// </summary>
/**
 * @return {string}
 */
function GetNoteName(nextNoteIndex)
{
    var noteIndex = GetNextNoteIndex(parseInt(nextNoteIndex + currentNote));
    var note = notes[noteIndex];
    currentNote = noteIndex;
    return note + octave;
}

/// <summary>
///     Calculates the index into Notes of the next item in the series.
///     Adjusts octave as required while keeping the result within the bounds (0..Notes.Count).
/// </summary>
/**
 * @return {int}
 */
function GetNextNoteIndex(index)
{
    var number = index;
    while (number < 0 || number > 11)
    {
        if (number >= 12)
        {
            number -= 12;
            if(octave < octaveHighLimit){
                ++octave;
            } else {
                octave = octaveHighLimit;
            }

        }
        else if (number < 0)
        {
            number += 12;
            if(octave > octaveLowLimit){
                --octave;
            } else {
                octave = octaveLowLimit;
            }
        }
    }

    return number;
}

/// <summary>
///     Call to start the generation!
///     Choose how many, where to start, and the octave.
/// </summary>
/**
 * @return {string[]}
 */
function Generate(total, start, oct){
    octave = oct;

    var numbers = [];
    var newNotes = [];

    //0 and 1 are always the same in the series and don't adhere to the formula. They are calculated ahead of the other entries.
    numbers[0] = 0;
    if (start <= 0)
    {
        newNotes[0] = GetSeriesEntry(0, numbers[0]);
    }

    numbers[1] = 1;
    if (start <= 1)
    {
        newNotes[1] = GetSeriesEntry(1, numbers[1]);
    }

    //j is used to add to the notes array, as it sometimes won't be synced with the i in the for loop.
    //if j is 0, the counting in the for loop with j starts at 0. This would override the below code for 0 and 1 outside the for loop, so this changes j accordingly.
    var j = start <= 1 ? 1 : 0;

    return GenerateRest(total, start, numbers, newNotes, j);
}

/// <summary>
///     Generate the rest of the series outside of 1 and 2.
/// </summary>
/**
 * @return {string[]}
 */
function GenerateRest(total,start,numbers,newNotes,j){
    var limit = start + total + 1;

    var notesList = newNotes;

    for (var i = 1; i < limit; i++)
    {
        var previousOct = octave;
        var index = 2 * i;
        var first = index - 2;
        var second = index - 1;
        var previous = i - 1;
        console.log("index = " + index + ", first = " + first + ", second = " + second + ", previous = " + previous);

        //breaks if at the end for the even number
        var nextIndex = index + 1;
        if (index >= start + total)
        {
            break;
        }

        //first (even) number entry
        numbers[index] = numbers[first] - (numbers[i] - numbers[previous]);

        if (index >= start)
        {

            notesList[2 * j] = GetSeriesEntry(index, numbers[index]);
        }

        //reset octave for next note
        octave = previousOct;

        //break if at the end for the odd number
        if (nextIndex >= start + total)
        {
            break;
        }

        //second (odd) number entry
        numbers[nextIndex] = numbers[second] + (numbers[i] - numbers[previous]);

        if (nextIndex >= start)
        {
            notesList[2 * j + 1] = GetSeriesEntry(nextIndex, numbers[nextIndex]);
            j++;
        }
    }

    return notesList;
}