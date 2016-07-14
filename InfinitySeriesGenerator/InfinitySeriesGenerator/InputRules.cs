﻿/* A set of rules involving keys pressed.
Author: Richard Gaynor
*/

namespace InfinitySeriesGenerator
{
    using System.Text.RegularExpressions;
    using System.Windows.Input;

    public static class InputRules
    {
        public static bool IsPermitted(Key inKey)
        {
            return InputRules.IsDelOrBackspace(inKey) ||
                inKey == Key.Tab || 
                InputRules.IsNumberKey(inKey) ||
                InputRules.IsArrowKey(inKey);
        }

        //stops people copy/pasting non-numbers in!
        public static string SanitiseInput(string s)
        {
            return Regex.Replace(s, @"[^\d]", "");
        }

        //checks for backspace/tab key
        private static bool IsDelOrBackspace(Key inKey)
        {
            return inKey == Key.Delete || inKey == Key.Back;
        }

        //true if a number, false if not
        private static bool IsNumberKey(Key inKey)
        {
            return inKey >= Key.D0 && inKey <= Key.D9 || inKey >= Key.NumPad0 && inKey <= Key.NumPad9;
        }

        private static bool IsArrowKey(Key inKey)
        {
            return inKey == Key.Left || inKey == Key.Right;
        }
    }
}
