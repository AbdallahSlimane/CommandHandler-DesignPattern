using System;

namespace ConsoleApp1
{
    public class CommandHandler
    {
        
        private readonly HashSet<string> validSpaceshipNames = new HashSet<string>
        {
            "Explorer",
            "Speeder",
            "Cargo"
        };
        
        public void ExecuteCommand(string commandLine)
        {
            string[] parts = commandLine.Split(new char[] { ' ' }, 2);
            string command = parts[0].ToUpper();
            string argument = parts.Length > 1 ? parts[1] : "";

            switch (command)
            {
                case "STOCK":
                    ProcessStockCommand(argument);
                    break;
                case "FINISHED":
                    ProcessFinishedCommand(argument);
                    break;
                case "NEEDED_STOCKS":
                    ProcessNeededStocksCommand(argument);
                    break;
                case "INSTRUCTIONS":
                    ProcessInstructionsCommand(argument);
                    break;
                case "VERIFY":
                    ProcessVerifyCommand(argument);
                    break;
                case "PRODUCE":
                    ProcessProduceCommand(argument);
                    break;
                case "ASSEMBLE":
                    ProcessAssembleCommand(argument);
                    break;
                case "GET_OUT_STOCK":
                    ProcessGetOutStockCommand(argument);
                    break;
                case "ERROR":
                case "AVAILABLE":
                case "UNAVAILABLE":
                case "STOCK_UPDATED":
                    ProcessSystemFeedback(command, argument);
                    break;
                default:
                    Console.WriteLine($"Unknown command: {command}");
                    break;
            }
        }

        /// 'out' keyword indicates that this parameter is expected to be modified by this method.
        private void ProcessGetOutStockCommand(string arguments)
        {
            if (TryParseArguments(arguments, out int quantity, out string partName))
            {
                Console.WriteLine($"Removed {quantity} units of {partName} from stock.");
            }
            else
            {
                Console.WriteLine("Invalid arguments. Usage: GET_OUT_STOCK [quantity] [partName]");
            }
        }

        /// 'out' keyword indicates that this parameter is expected to be modified by this method.
        private void ProcessSystemFeedback(string command, string argument)
        {
            switch (command)
            {
                case "ERROR":
                    Console.WriteLine($"Error: {argument}");
                    break;
                case "AVAILABLE":
                    Console.WriteLine("The requested item is available.");
                    break;
                case "UNAVAILABLE":
                    Console.WriteLine("The requested item is not available.");
                    break;
                case "STOCK_UPDATED":
                    Console.WriteLine("Stock has been updated successfully.");
                    break;
            }
        }
        private void ProcessAssembleCommand(string arguments)
        {
            var args = arguments.Split(' ');
            if (args.Length >= 3)
            {
                string assemblyName = args[0];
                string part1 = args[1];
                string part2 = args[2];
                Console.WriteLine($"Assembling {assemblyName} from {part1} and {part2}");
            }
            else
            {   
                Console.WriteLine("Invalid arguments. Usage: ASSEMBLE [assemblyName] [part1] [part2]");
            }
        }

        private void ProcessStockCommand(string argument)
        {
            if (!string.IsNullOrEmpty(argument))
            {
                Console.WriteLine("Error: The 'STOCK' command does not take any arguments.");
            }
            else
            {
                Console.WriteLine("Displaying stock: No inventory management in this demo.");
            }
        }

        private void ProcessFinishedCommand(string argument)
        {
            if (string.IsNullOrEmpty(argument))
            {
                Console.WriteLine("Error: The 'FINISHED' command requires the name of a spaceship as an argument.");
            }
            else
            {
                Console.WriteLine($"Spaceship {argument} marked as finished");
            }
        }

        private void ProcessNeededStocksCommand(string arguments)
        {
            if (TryParseArguments(arguments, out int quantity, out string spaceshipName) && IsValidSpaceshipName(spaceshipName))
            {
                Console.WriteLine($"Calculating needed stocks for {quantity} units of {spaceshipName}");
            }
            else
            {
                Console.WriteLine("Invalid arguments. Usage: NEEDED_STOCKS [quantity] [spaceshipName]");
            }
        }

        private void ProcessInstructionsCommand(string arguments)
        {
            if (TryParseArguments(arguments, out int quantity, out string spaceshipName) && IsValidSpaceshipName(spaceshipName))
            {
                Console.WriteLine($"Generating assembly instructions for {quantity} units of {spaceshipName}");
            }
            else
            {
                Console.WriteLine("Invalid arguments. Usage: INSTRUCTIONS [quantity] [spaceshipName]");
            }
        }

        private void ProcessVerifyCommand(string arguments)
        {
            if (TryParseArguments(arguments, out int quantity, out string spaceshipName) && IsValidSpaceshipName(spaceshipName))
            {
                Console.WriteLine($"Verifying the possibility of fulfilling the order for {quantity} units of {spaceshipName}");
            }
            else
            {
                Console.WriteLine("Invalid arguments. Usage: VERIFY [quantity] [spaceshipName]");
            }
        }

        private void ProcessProduceCommand(string arguments)
        {
            if (TryParseArguments(arguments, out int quantity, out string spaceshipName) && IsValidSpaceshipName(spaceshipName))
            {
                Console.WriteLine($"Producing the order for {quantity} units of {spaceshipName}");
            }
            else
            {
                Console.WriteLine("Invalid arguments. Usage: PRODUCE [quantity] [spaceshipName]");
            }
        }
        
        private bool IsValidSpaceshipName(string name)
        {
            return validSpaceshipNames.Contains(name);
        }

        /// <summary>
        /// Tries to parse a string for a quantity and a part or spaceship name.
        /// </summary>
        /// <param name="arguments">The string containing the arguments to parse.</param>
        /// <param name="quantity">Output parameter to store the parsed quantity. 
        /// 'out' keyword indicates that this parameter is expected to be modified by this method.</param>
        /// <param name="spaceshipName">Output parameter to store the parsed part or spaceship name provided by the caller.</param>
        /// <returns>True if both quantity and name could be parsed successfully, false otherwise.</returns>
        private bool TryParseArguments(string arguments, out int quantity, out string spaceshipName)
        {
            quantity = 0;
            spaceshipName = null;

            var args = arguments.Split(' ');
            if (args.Length == 2 && int.TryParse(args[0], out quantity))
            {
                spaceshipName = args[1];
                return true;
            }
            return false;
        }
    }
}
