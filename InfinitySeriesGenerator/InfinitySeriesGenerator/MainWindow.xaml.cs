namespace InfinitySeriesGenerator
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    using Microsoft.Win32;

    public partial class MainWindow
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        //checks for numerical input
        private void NumberBoxPreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !InputRules.IsPermitted(e.Key);
        }

        //stops people copy/pasting non-numbers in!
        private void NumberBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            this.seriesSizeInput.Text = InputRules.SanitiseInput(this.seriesSizeInput.Text);
        }

        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            var seriesSize = Convert.ToInt32(this.seriesSizeInput.Text);
            var startIndex = Convert.ToInt32(this.startingIndexInput.Text);
            var generator = new Generator(this.NoteNamesBox.SelectedIndex);
            var notes = generator.GenerateSeries(seriesSize, startIndex);
            MainWindow.WriteToFile(notes);
        }

        //loads the notes into the combobox
        private void SetupStartingNoteNameSelector(object sender, RoutedEventArgs e)
        {
            // Get the ComboBox reference.
            var comboBox = sender as ComboBox;

            if (comboBox == null)
            {
                return;
            }

            // Assign the ItemsSource to the List.
            comboBox.ItemsSource = NoteCalculator.Notes;

            // Make the first item selected.
            comboBox.SelectedIndex = 0;
        }

        private static void WriteToFile(IEnumerable<string> notes)
        {
            //Saves the series as a text file
            var dialog = new SaveFileDialog
            {
                FileName = "InfinitySeries",
                DefaultExt = ".txt",
                Filter = "Text documents (.txt)|*.txt"
            };

            if (dialog.ShowDialog().GetValueOrDefault())
            {
                var filename = dialog.FileName;
                File.WriteAllLines(filename, notes);
            }
        }
    }
}
