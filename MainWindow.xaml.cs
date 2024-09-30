/*
    Name: Thi Ty Tran
    Date Created: Sep 22, 2024
    Description: Messages Sent App
    Last modified: Sep 28, 2024
 */

using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MessagesSent
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //GLOBAL Variables
        int DayCounter = 0; // Counter for the day
        double totalMessages = 0.0; // Sum of messages for 7 days

        public MainWindow()
        {
            InitializeComponent();
            userInputTxtBox.Focus(); // Set focus on the user input textbox
        }

        /// <summary>
        /// This function processes user input, validates it, updates the display, 
        /// and calculates the average if the maximum number of input days is reached.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void enterBtn_Click(object sender, RoutedEventArgs e)
        {
            // Constants and Variables
            const int minValue = 0;
            const int maxValue = 10000;
            const int maxDay = 7;

            // Get input from the user
            string userInput = userInputTxtBox.Text;

            // Do Validations
            // Check if input is not blank
            if (isInputNotBlank(userInput))
            {
                // Check if input is a valid number
                if (isValidDouble(userInput))
                {
                    double userInputDouble = Convert.ToDouble(userInput);
                    // Check if input is within the valid range
                    if (isValidRange(minValue, maxValue, userInputDouble))
                    {
                        // Add input to multiline textbox
                        multiLineTxtBox.Text += $"Day {DayCounter + 1}: {userInputDouble}\n";

                        // Increment total and day count
                        totalMessages += userInputDouble;
                        DayCounter++;

                        if (DayCounter < maxDay)
                        {
                            // Update Day label
                            DayCounterLbl.Content = "Day " + (DayCounter + 1);
                            userInputTxtBox.Clear();
                            userInputTxtBox.Focus();
                        }
                        else
                        {
                            // Disable input and calculate average
                            enterBtn.IsEnabled = false;
                            userInputTxtBox.IsEnabled = false;
                            double average = totalMessages / maxDay;
                            // Display the calculated average
                            messagesAverageResultLbl.Text = $"Messages per day: {average:F1}";

                            // Set focus on Reset button
                            resetBtn.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Input is not within range (0-10000). Please try again!");
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a number between 0 and 10000.");
                }
            }
            else
            {
                MessageBox.Show("Input cannot be blank.");
            }
        }

        // Validation methods
        /// <summary>
        /// This function will validate if user input is null or empty
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private bool isInputNotBlank(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// This function will validate double range
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private bool isValidDouble(string input)
        {
            if (double.TryParse(input, out double result))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// This function will validate double range
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <param name="input"></param>
        private bool isValidRange(int minValue, int maxValue, double input)
        {
            if (input >= minValue && input <= maxValue)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// This function is to clear all user inputs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void resetBtn_Click(object sender, RoutedEventArgs e)
        {
            // Reset all fields and variables
            DayCounter = 0;
            totalMessages = 0.0;
            DayCounterLbl.Content = "Day 1";
            userInputTxtBox.Clear();
            multiLineTxtBox.Clear();
            messagesAverageResultLbl.Clear();

            // Enable input again
            enterBtn.IsEnabled = true;
            userInputTxtBox.IsEnabled = true;
            userInputTxtBox.Focus();
        }

        /// <summary>
        /// This function is to exit the application when the Exit button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {
            // Exit the application
            this.Close();
        }
    }
}