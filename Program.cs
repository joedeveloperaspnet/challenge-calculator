using System;

/// <summary>
/// Restarurant365 Code Challenge: String Calculator
/// Candidate: Joseph DiPierro
/// Created: May 13, 2022
/// Updated: May 14, 2022
/// </summary>
namespace calculatorChallenge
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.CancelKeyPress += new ConsoleCancelEventHandler(Console_CancelKeyPress);

            Calculator calculator = new Calculator();

            DisplayAvailableDefaultArguements(calculator);

            // added for stretch goal #3

            SetAndDisplayConsoleArguements(args, calculator);

            GetSetAndDisplayCommandLineArguements(calculator);

            GetAndProcessCalculatorAddition(calculator);

            UnitTests.RunUnitTests();

            Console.WriteLine();
            Console.WriteLine("Press enter key to exit");

            Console.ReadLine();

             Console.CancelKeyPress -= new ConsoleCancelEventHandler(Console_CancelKeyPress);
        }

        /// <summary>
        /// Display the available arguements (console and command line
        /// (1) Deny nagative numbers, (2) upper bound, or (3) alturnate delimiter
        /// </summary>
        static void DisplayAvailableDefaultArguements(Calculator calculator)
        {
            Console.WriteLine();
            Console.WriteLine(" Available Arguements");
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine( " Deny negative numbers (boolean) default = " +  calculator.Arguements.DenyNegativeNumbers.ToString());
            Console.WriteLine(" Upper bound (integer) default = " + calculator.Arguements.UpperBound.ToString());
            Console.WriteLine(" Alturnate delimiter (string) predefined = , \\n");
            Console.WriteLine(" For example:  |?| 100 True");
            Console.WriteLine("---------------------------------------------");
        }

        /// <summary>
        /// Allow console command line arguements to be specified for
        /// (1) Deny nagative numbers, (2) upper bound, or (3) alturnate delimiter
        /// and set the calculator properties if specified
        /// </summary>
        /// <param name="args"></param>
        /// <param name="calculator"></param>
        static void SetAndDisplayConsoleArguements(string[] args, Calculator calculator)
        {
            if (args.Length == 0)
            {
                Console.WriteLine(" No console arguements specified");
            }
            else
            {
                Console.WriteLine("Specified Console Arguements");
                Console.WriteLine("---------------------------------------------");

                SetAndDisplayArguements(args, calculator);
            }

            Console.WriteLine("---------------------------------------------");
        }

        /// <summary>
        /// Allow the user to specify up to 3 command line arguements for
        /// (1) Deny nagative numbers, (2) upper bound, or (3) alturnate delimiter
        /// and set the calculator properties if specified
        /// </summary>
        /// <param name="args"></param>
        /// <param name="calculator"></param>
        static void GetSetAndDisplayCommandLineArguements(Calculator calculator)
        {
            string input = String.Empty;

            Console.WriteLine();
            Console.Write("Enter arguements (optional): ");
            input = Console.ReadLine();

            if (!String.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine();

                string[] commandLineArgs  = input.Split(' ');

                SetAndDisplayArguements(commandLineArgs, calculator);

                Console.WriteLine("---------------------------------------------");
            }
        }

        /// <summary>
        ///  set the calculator properties from the arguements, if specified,  and display the values
        /// </summary>
        /// <param name="args"></param>
        /// <param name="calculator"></param>
        private static void SetAndDisplayArguements(string[] args, Calculator calculator)
        {
            bool denyNegativeNumbers = false;
            int upperBound = 0;

            foreach (string arg in args)
            {
                if (bool.TryParse(arg, out denyNegativeNumbers))
                {
                    calculator.Arguements.DenyNegativeNumbers = denyNegativeNumbers;
                    Console.WriteLine("Deny negative numbers = " + denyNegativeNumbers.ToString());
                }
                else if (int.TryParse(arg, out upperBound))
                {
                    calculator.Arguements.UpperBound = upperBound;
                    Console.WriteLine("Upper bound = " + upperBound.ToString());
                }
                else
                {
                    calculator.Arguements.AlturnateDelimiter = arg;
                    Console.WriteLine("Alturnate delimiter = " + arg);
                }
            }
        }

        /// <summary>
        /// Get numbers to add or Ctrl + C when finished
        /// </summary>
        /// <param name="calculator"></param>
        static void GetAndProcessCalculatorAddition(Calculator calculator)
        {
            string input = String.Empty;

            Console.WriteLine();
            Console.WriteLine("Enter input to add. Enter Ctrl + C when finished.");

            try
            {
                do
                {
                    Console.WriteLine();
                    Console.Write("Enter Integer: ");

                    input = Console.ReadLine();

                    if (input == null)
                    {
                        // Ctrl + C was entered
                        break;
                    }

                    calculator.Add(input);

                    Console.WriteLine("Total: " + calculator.TotalResult);

                } while (true);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
                Console.ReadLine();
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Handle the Ctrl + C event to allow continued execution
        /// Code borrowed from http://www.blackwasp.co.uk/CaptureConsoleBreak.aspx
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine("Finished");
            Console.WriteLine();

            if (e.SpecialKey == ConsoleSpecialKey.ControlC)
            {
                e.Cancel = true;
            }
        }
    }
}

