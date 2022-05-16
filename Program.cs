using System;

/// <summary>
/// Restarurant365 Code Challenge: String Calculator
/// Candidate: Joseph DiPierro
/// Created: May 13, 2022
/// Last Updated: May 15, 2022
/// </summary>
namespace calculatorChallenge
{
    internal class Program
    {
        static bool Canceled = false;

        static void Main(string[] args)
        {
            Console.CancelKeyPress += new ConsoleCancelEventHandler(Console_CancelKeyPress);

            Calculator calculator = new Calculator();

            DisplayAvailableDefaultArguements(calculator);

            // added for stretch goal #3
            SetAndDisplayConsoleArguements(args, calculator);

            GetSetAndDisplayCommandLineArguements(calculator);

            // modified for stretch goal #5
            GetAndProcessCalculatorOperations(calculator);

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
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine( " Deny negative numbers (boolean) default = " +  calculator.Arguements.DenyNegativeNumbers.ToString());
            Console.WriteLine(" Upper bound (integer) default = " + calculator.Arguements.UpperBound.ToString());
            Console.WriteLine(" Alturnate delimiter (string) predefined = , \\n");
            Console.WriteLine(" For example:  |?| 100 True");
            Console.WriteLine("----------------------------------------------------");
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
                Console.WriteLine("----------------------------------------------------");

                SetAndDisplayArguements(args, calculator);
            }

            Console.WriteLine("----------------------------------------------------");
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
            Console.Write("Enter arguement(s) (optional): ");
            input = Console.ReadLine();

            if (!String.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine();

                string[] commandLineArgs  = input.Split(' ');

                SetAndDisplayArguements(commandLineArgs, calculator);
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
        static void GetAndProcessCalculatorOperations(Calculator calculator)
        {
            OperationType operation;
            ConsoleKeyInfo operationInputKeyInfo;
            string input = String.Empty;

            Console.WriteLine();
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine(" Numbers can be entered directly, comma separated, or");
            Console.WriteLine(" in this format:  //[{delimiter1}][{delimiter2}]...\\n{numbers}");
            Console.WriteLine(" Valid operator types are:  +  -  /  *");
            Console.WriteLine(" Enter Ctrl + C when finished.");
            Console.WriteLine("----------------------------------------------------------------");

            do
            {
                Console.WriteLine();
                Console.Write("Enter operator type: ");

                operationInputKeyInfo = Console.ReadKey();

                if (Canceled)
                {
                    break;   // Ctrl + C was entered
                }

                if (Utilities.TryParse(operationInputKeyInfo.KeyChar, out operation))
                {
                    calculator.Arguements.Operation = operation;

                    Console.WriteLine(" " + Utilities.GetOperationTypeName(calculator.Arguements.Operation));
                    Console.Write("Enter delimeters/numbers input: ");

                    input = Console.ReadLine();

                    if (input == null)
                    {
                        break;  // Ctrl + C was entered
                    }

                    try
                    {
                        calculator.Calculate(input);

                        Console.WriteLine("Formula: " + calculator.TotalResult);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error: " + e.Message);
                        Console.ReadLine();
                    }
                }
                else
                {
                    Console.WriteLine();
                    Console.Write("Error: Invalid operator type!");
                    Console.WriteLine();
                }
            } while (true);

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
                Canceled = true;
            }
        }
    }
}

